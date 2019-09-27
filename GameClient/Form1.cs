using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using GameServer;
using GameServer.Models;
using GameServer.Messages;
using WebSocketSharp;

namespace GameClient
{
    public partial class Form1 : Form
    {
        Bitmap BackBuffer;
        Bitmap FrontBuffer;

        //Score management
        int bestScore = 0; //SERVER VAR
        string scoreTextBox;

        Player currentPlayer = null;
        GamePlayer currentGamePlayer = null;
        Game currentGame = null;
        List<Enemy> enemies = new List<Enemy>();

        WebSocket webSocket;
        private readonly object x = new object();

        public int getDistance(double value)
        {
            return (int)(value * ClientSize.Width / 100);
        }

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            // Go full screen with fixed square size
            Text = "JDDestroyer";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            int ScreenHeight = (int)(Screen.PrimaryScreen.Bounds.Height * 0.6);
            ClientSize = new Size(ScreenHeight, ScreenHeight);

            //Set up the updating
            Timer GameTimer = new Timer();
            GameTimer.Interval = 10;
            GameTimer.Tick += new EventHandler(Tick);
            GameTimer.Start();
            Load += new EventHandler(Init);
            Paint += new PaintEventHandler(FormPaint);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            Connect();
            //  InitData();
        }

        void Connect()
        {
            webSocket = new WebSocket(Config.SERVER_HOST + ":" + Config.SERVER_PORT);

            webSocket.OnMessage += parseMessage;

            webSocket.Connect();
        }

        void parseMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Geting");
            Console.WriteLine(e.Data);
            SocketMessage bsObj = JsonConvert.DeserializeObject<SocketMessage>(e.Data);

            switch (bsObj.type)
            {
                case EnemySpawnMessage.TYPE:

                    if (currentGame != null)
                    {
                        EnemySpawnMessage enemyData = JsonConvert.DeserializeObject<EnemySpawnMessage>(bsObj.data);

                        lock (x)
                        {
                            switch (enemyData.type)
                            {
                                default:
                                case RedEnemy.TYPE:
                                    enemies.Add(new RedEnemy(enemyData.position));
                                    break;
                                case GreenEnemy.TYPE:
                                    enemies.Add(new GreenEnemy(enemyData.position));
                                    break;
                                case BlueEnemy.TYPE:
                                    enemies.Add(new BlueEnemy(enemyData.position));
                                    break;

                            }
                        }
                    }
                    break;

                case PositionChangedMessage.TYPE:
                    PositionChangedMessage positionChange = JsonConvert.DeserializeObject<PositionChangedMessage>(bsObj.data);

                    GamePlayer gamePlayer = currentGame.getPlayer(positionChange.playerId);
                    gamePlayer.position = positionChange.position;
                    break;



                case PlayerDataMessage.TYPE:
                    PlayerDataMessage playerData = JsonConvert.DeserializeObject<PlayerDataMessage>(bsObj.data);
                    if (currentPlayer == null)
                    {
                        currentPlayer = new Player(playerData.id, playerData.name);
                        currentGamePlayer = new GamePlayer(currentPlayer);
                        currentGamePlayer.game = currentGame;
                        currentGamePlayer.position = playerData.position;
                        connectPlayerWithGame();
                    }
                    break;



                case GameDataMessage.TYPE:
                    Game game = JsonConvert.DeserializeObject<Game>(bsObj.data);
                    if (currentGame == null)
                    {
                        currentGame = new Game();
                    }
                    currentGame.name = game.name;


                    if (game.P1 != null)
                    {
                        currentGame.P1 = game.P1;
                        currentGame.P1.moveStrategy = new P1MoveStrategy();
                    }
                    if (game.P2 != null)
                    {
                        currentGame.P2 = game.P2;
                        currentGame.P2.moveStrategy = new P2MoveStrategy();
                    }
                    if (game.P3 != null)
                    {
                        currentGame.P3 = game.P3;
                        currentGame.P3.moveStrategy = new P3MoveStrategy();
                    }
                    if (game.P4 != null)
                    {
                        currentGame.P4 = game.P4;
                        currentGame.P4.moveStrategy = new P4MoveStrategy();
                    }
                    currentGame.enemies = new List<Enemy>();
                    if (currentGamePlayer != null)
                    {
                        currentGamePlayer.game = currentGame;
                    }
                    connectPlayerWithGame();
                    break;
            }
        }

        void connectPlayerWithGame()
        {
            if (currentGame != null && currentPlayer != null && currentGamePlayer != null)
            {
                if (currentGame.P1 != null && currentGame.P1.Equals(currentGamePlayer))
                {
                    currentGamePlayer.moveStrategy = currentGame.P1.moveStrategy;
                    currentGame.P1 = currentGamePlayer;
                    return;
                }

                if (currentGame.P2 != null && currentGame.P2.Equals(currentGamePlayer))
                {
                    currentGamePlayer.moveStrategy = currentGame.P2.moveStrategy;
                    currentGame.P2 = currentGamePlayer;
                    return;
                }
                if (currentGame.P3 != null && currentGame.P3.Equals(currentGamePlayer))
                {
                    currentGamePlayer.moveStrategy = currentGame.P3.moveStrategy;
                    currentGame.P3 = currentGamePlayer;
                    return;
                }
                if (currentGame.P4 != null && currentGame.P4.Equals(currentGamePlayer))
                {
                    currentGamePlayer.moveStrategy = currentGame.P4.moveStrategy;
                    currentGame.P4 = currentGamePlayer;
                    return;
                }
            }
        }


        void sendMessage(string data)
        {
            if (webSocket != null)
            {
                Console.WriteLine("Sending");
                Console.WriteLine(data);

                webSocket.Send(data);
            }
        }

        //Controls
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                currentGamePlayer.MoveLeft();
                SocketMessage message = new SocketMessage();
                message.type = MoveLeftMessage.TYPE;
                sendMessage(JsonConvert.SerializeObject(message));
            }
            else if (e.KeyCode == Keys.Right)
            {
                currentGamePlayer.MoveRight();
                SocketMessage message = new SocketMessage();
                message.type = MoveRightMessage.TYPE;
                sendMessage(JsonConvert.SerializeObject(message));
            }
            else if (e.KeyCode == Keys.Space)
                bestScore += 1;
        }

        //Update paint
        void FormPaint(object sender, PaintEventArgs e)
        {
            if (BackBuffer != null)
            {
                e.Graphics.DrawImageUnscaled(BackBuffer, Point.Empty);
                e.Graphics.DrawImageUnscaled(FrontBuffer, Point.Empty);
            }
        }

        void Init(object sender, EventArgs ev)
        {
            if (BackBuffer != null)
            {
                BackBuffer.Dispose();
                FrontBuffer.Dispose();
            }
            BackBuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
            FrontBuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
            using (var e = Graphics.FromImage(BackBuffer))
            {
                int length = ClientSize.Width;
                //Main canvas color
                e.FillRectangle(Brushes.WhiteSmoke, new Rectangle(0, 0, getDistance(100), length));
                //Middle square

                Rectangle middleSquare = new Rectangle(getDistance(Config.CORNERSIZE), getDistance(Config.CORNERSIZE), getDistance(Config.INNERSQUARESIZE), getDistance(Config.INNERSQUARESIZE));
                e.FillRectangle(Brushes.Gray, middleSquare);
                //Add border
                Pen borderPen = new Pen(Color.FromArgb(105, 105, 105), 4);
                borderPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                e.DrawRectangle(borderPen, middleSquare);
                //Corner squares
                int cornerCord = getDistance(Config.CORNERSIZE + Config.INNERSQUARESIZE);
                e.FillRectangle(Brushes.Black, new Rectangle(0, 0, getDistance(Config.CORNERSIZE), getDistance(Config.CORNERSIZE)));
                e.FillRectangle(Brushes.Black, new Rectangle(0, cornerCord, getDistance(Config.CORNERSIZE), getDistance(Config.CORNERSIZE)));
                e.FillRectangle(Brushes.Black, new Rectangle(cornerCord, 0, getDistance(Config.CORNERSIZE), getDistance(Config.CORNERSIZE)));
                e.FillRectangle(Brushes.Black, new Rectangle(cornerCord, cornerCord, getDistance(Config.CORNERSIZE), getDistance(Config.CORNERSIZE)));
                //Score title
                e.DrawString("Best score:", new Font("Comic Sans MS", 18), Brushes.White, (float)(length * 0.75), 0);
            }
        }

        public void drawEnemy(Enemy enemy)
        {
            if (FrontBuffer != null)
            {
                using (var graphic = Graphics.FromImage(FrontBuffer))
                {
                    graphic.FillRectangle(new SolidBrush(enemy.getColor()), getDistance(enemy.position.x) - getDistance(Config.ENEMYSIZE / 2), getDistance(enemy.position.y) - getDistance(Config.ENEMYSIZE / 2), getDistance(Config.ENEMYSIZE), getDistance(Config.ENEMYSIZE));
                }
            }
        }

        void Draw()
        {
            if (BackBuffer != null)
            {
                using (var graphic = Graphics.FromImage(FrontBuffer))
                {
                    graphic.Clear(Color.Transparent);
                    //Update the score
                }
                Invalidate();
            }
            DrawPlayers();
        }

        void DrawPlayers()
        {
            if (FrontBuffer != null && currentGame != null && currentGamePlayer != null && currentPlayer != null)
            {
                using (var graphic = Graphics.FromImage(FrontBuffer))
                {
                    foreach (GamePlayer gamePlayer in currentGame.getPlayers())
                    {
                        if (gamePlayer != null)
                        {
                            Brush color = Brushes.Red;
                            if (gamePlayer.player.id == currentPlayer.id)
                            {
                                color = Brushes.Green;
                            }
                            graphic.FillRectangle(color, getDistance(gamePlayer.position.x) - getDistance(Config.PLAYERSIZE / 2), getDistance(gamePlayer.position.y) - getDistance(Config.PLAYERSIZE / 2), getDistance(Config.PLAYERSIZE), getDistance(Config.PLAYERSIZE));
                        }
                    }
                    lock (x)
                    {
                        foreach (Enemy enemy in enemies)
                        {
                            drawEnemy(enemy);
                        }
                    }

                    graphic.DrawString(scoreTextBox, new Font("Comic Sans MS", 18), Brushes.White, (float)(ClientSize.Width * 0.8), 28);
                }
                Invalidate();
            }
        }

        void Tick(object sender, EventArgs e)
        {
            if (currentPlayer != null)
            {
                scoreTextBox = currentPlayer.name + ": " + bestScore.ToString();
                Draw();
            }
        }
    }
}
