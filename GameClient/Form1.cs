using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;
using System.Windows.Forms;
using GameServer;
using GameServer.Geometry;
using GameServer.Models;
using GameServer.Messages;
using WebSocketSharp;

namespace GameClient
{
    public partial class Form1 : Form
    {
        Bitmap BackBuffer;
        Bitmap FrontBuffer;

        int bestScore = 0;
        string scoreTextBox;

        Player currentPlayer = null;
        GamePlayer currentGamePlayer = null;
        GameFacade currentGame = null;
        List<Enemy> enemies = new List<Enemy>();

        WebSocket webSocket;
        private readonly object x = new object();
        private readonly object bulletLock = new object();

        List<Bullet> bullets = new List<Bullet>();

        public int GetDistance(double value)
        {
            return (int)(value * ClientSize.Width / 100);
        }

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            Text = "JDDestroyer";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            int ScreenHeight = (int)(Screen.PrimaryScreen.Bounds.Height * 0.6);
            ClientSize = new Size(ScreenHeight, ScreenHeight);
            Timer GameTimer = new Timer
            {
                Interval = 10
            };
            GameTimer.Tick += new EventHandler(Tick);
            GameTimer.Start();
            Load += new EventHandler(Init);
            Paint += new PaintEventHandler(FormPaint);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            Connect();
        }

        void Connect()
        {
            webSocket = new WebSocket(Config.SERVER_HOST + ":" + Config.SERVER_PORT);
            webSocket.OnMessage += ParseMessage;
            webSocket.Connect();
        }

        void ParseMessage(object sender, MessageEventArgs e)
        {
            SocketMessage bsObj = JsonConvert.DeserializeObject<SocketMessage>(e.Data);
            switch (bsObj.type)
            {
                case EnemiesDataMessage.TYPE:
                    EnemiesDataMessage enemiesData = JsonConvert.DeserializeObject<EnemiesDataMessage>(bsObj.data);
                    lock (x)
                    {
                        enemies = new List<Enemy>();
                        if (enemiesData.enemiesList != null)
                        {
                            foreach(EnemyDummy dummy in enemiesData.enemiesList)
                            {
                                enemies.Add(Enemy.createFromDummy(dummy));
                            }
                        }
                    }
                    break;

                case PositionChangedMessage.TYPE:
                    PositionChangedMessage positionChange = JsonConvert.DeserializeObject<PositionChangedMessage>(bsObj.data);

                    GamePlayer gamePlayer = currentGame.gamePlayers.getPlayer(positionChange.playerId);
                    gamePlayer.position = positionChange.position;
                    break;

                case BulletsDataMessage.TYPE:
                   // Console.WriteLine("bullet list");
                    BulletsDataMessage bulletsData = JsonConvert.DeserializeObject<BulletsDataMessage>(bsObj.data);
                    lock (bulletLock)
                    {
                        bullets = new List<Bullet>();
                        if (bulletsData.bulletList != null)
                        {
                            bullets = bulletsData.bulletList;
                        }
                    }
                    break;
                case PlayerDataMessage.TYPE:
                    PlayerDataMessage playerData = JsonConvert.DeserializeObject<PlayerDataMessage>(bsObj.data);
                    Console.WriteLine(playerData.name);
                    if (currentPlayer == null)
                    {
                        currentPlayer = new Player(playerData.id, playerData.name);
                        currentGamePlayer = new GamePlayer(currentPlayer)
                        {
                            game = currentGame,
                            position = playerData.position,
                            color = playerData.color
                        };
                        ConnectPlayerWithGame();
                    }
                    break;
                case GameDataMessage.TYPE:
                    GameFacade game = JsonConvert.DeserializeObject<GameFacade>(bsObj.data);
                    if (currentGame == null)
                    {
                        currentGame = new GameFacade();
                    }
                    currentGame.name = game.name;
                    if (game.gamePlayers.P1 != null)
                    {
                        currentGame.gamePlayers.P1 = game.gamePlayers.P1;
                        IMoveStrategy strategy = new P1MoveStrategy();
                        currentGame.gamePlayers.P1.moveLeft = new MoveLeftCommand(strategy, game.gamePlayers.P1.position);
                        currentGame.gamePlayers.P1.moveRight = new MoveRightCommand(strategy, game.gamePlayers.P1.position);
                    }
                    if (game.gamePlayers.P2 != null)
                    {
                        currentGame.gamePlayers.P2 = game.gamePlayers.P2;
                        IMoveStrategy strategy = new P2MoveStrategy();
                        currentGame.gamePlayers.P2.moveLeft = new MoveLeftCommand(strategy, game.gamePlayers.P2.position);
                        currentGame.gamePlayers.P2.moveRight = new MoveRightCommand(strategy, game.gamePlayers.P2.position);
                    }
                    if (game.gamePlayers.P3 != null)
                    {
                        currentGame.gamePlayers.P3 = game.gamePlayers.P3;
                        IMoveStrategy strategy = new P3MoveStrategy();
                        currentGame.gamePlayers.P3.moveLeft = new MoveLeftCommand(strategy, game.gamePlayers.P3.position);
                        currentGame.gamePlayers.P3.moveRight = new MoveRightCommand(strategy, game.gamePlayers.P3.position);
                    }
                    if (game.gamePlayers.P4 != null)
                    {
                        currentGame.gamePlayers.P4 = game.gamePlayers.P4;
                        IMoveStrategy strategy = new P4MoveStrategy();
                        currentGame.gamePlayers.P4.moveLeft = new MoveLeftCommand(strategy, game.gamePlayers.P4.position);
                        currentGame.gamePlayers.P4.moveRight = new MoveRightCommand(strategy, game.gamePlayers.P4.position);
                    }
                    if (currentGamePlayer != null)
                    {
                        currentGamePlayer.game = currentGame;
                    }
                    ConnectPlayerWithGame();
                    break;
            }
        }

        void ConnectPlayerWithGame()
        {
            if (currentGame != null && currentPlayer != null && currentGamePlayer != null)
            {
                if (currentGame.gamePlayers.P1 != null && currentGame.gamePlayers.P1.Equals(currentGamePlayer))
                {
                    currentGamePlayer.moveLeft = currentGame.gamePlayers.P1.moveLeft;
                    currentGamePlayer.moveRight = currentGame.gamePlayers.P1.moveRight;
                    currentGame.gamePlayers.P1 = currentGamePlayer;
                    return;
                }

                if (currentGame.gamePlayers.P2 != null && currentGame.gamePlayers.P2.Equals(currentGamePlayer))
                {
                    currentGamePlayer.moveLeft = currentGame.gamePlayers.P2.moveLeft;
                    currentGamePlayer.moveRight = currentGame.gamePlayers.P2.moveRight;
                    currentGame.gamePlayers.P2 = currentGamePlayer;
                    return;
                }
                if (currentGame.gamePlayers.P3 != null && currentGame.gamePlayers.P3.Equals(currentGamePlayer))
                {
                    currentGamePlayer.moveLeft = currentGame.gamePlayers.P3.moveLeft;
                    currentGamePlayer.moveRight = currentGame.gamePlayers.P3.moveRight;
                    currentGame.gamePlayers.P3 = currentGamePlayer;
                    return;
                }
                if (currentGame.gamePlayers.P4 != null && currentGame.gamePlayers.P4.Equals(currentGamePlayer))
                {
                    currentGamePlayer.moveLeft = currentGame.gamePlayers.P4.moveLeft;
                    currentGamePlayer.moveRight = currentGame.gamePlayers.P4.moveRight;
                    currentGame.gamePlayers.P4 = currentGamePlayer;
                    return;
                }
            }
        }


        void SendMessage(string data)
        {
            if (webSocket != null)
            {
                webSocket.Send(data);
            }
        }

        //Controls
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                if (currentGamePlayer.MoveLeft())
                {
                    SocketMessage message = new SocketMessage
                    {
                        type = MoveLeftMessage.TYPE
                    };
                    SendMessage(JsonConvert.SerializeObject(message));
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (currentGamePlayer.MoveRight())
                {
                    SocketMessage message = new SocketMessage
                    {
                        type = MoveRightMessage.TYPE
                    };
                    SendMessage(JsonConvert.SerializeObject(message));
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                SocketMessage message = new SocketMessage
                {
                    type = ShootMessage.TYPE
                };
                SendMessage(JsonConvert.SerializeObject(message));
            }
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
                e.FillRectangle(Brushes.WhiteSmoke, new Rectangle(0, 0, GetDistance(100), length));
                //Middle square

                Rectangle middleSquare = new Rectangle(GetDistance(Config.CORNERSIZE), GetDistance(Config.CORNERSIZE), GetDistance(Config.INNERSQUARESIZE), GetDistance(Config.INNERSQUARESIZE));
                e.FillRectangle(Brushes.Gray, middleSquare);
                //Add border
                Pen borderPen = new Pen(Color.FromArgb(105, 105, 105), 4)
                {
                    Alignment = System.Drawing.Drawing2D.PenAlignment.Inset
                };
                e.DrawRectangle(borderPen, middleSquare);
                //Corner squares
                int cornerCord = GetDistance(Config.CORNERSIZE + Config.INNERSQUARESIZE);
                e.FillRectangle(Brushes.Black, new Rectangle(0, 0, GetDistance(Config.CORNERSIZE), GetDistance(Config.CORNERSIZE)));
                e.FillRectangle(Brushes.Black, new Rectangle(0, cornerCord, GetDistance(Config.CORNERSIZE), GetDistance(Config.CORNERSIZE)));
                e.FillRectangle(Brushes.Black, new Rectangle(cornerCord, 0, GetDistance(Config.CORNERSIZE), GetDistance(Config.CORNERSIZE)));
                e.FillRectangle(Brushes.Black, new Rectangle(cornerCord, cornerCord, GetDistance(Config.CORNERSIZE), GetDistance(Config.CORNERSIZE)));
                //Score title
                e.DrawString("Best score:", new Font("Comic Sans MS", 18), Brushes.White, (float)(length * 0.75), 0);
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
                    foreach (GamePlayer gamePlayer in currentGame.gamePlayers.getPlayers())
                    {
                        if (gamePlayer != null)
                        {
                            graphic.FillRectangle(new SolidBrush(gamePlayer.color), GetDistance(gamePlayer.position.x) - GetDistance(Config.PLAYERSIZE / 2), GetDistance(gamePlayer.position.y) - GetDistance(Config.PLAYERSIZE / 2), GetDistance(Config.PLAYERSIZE), GetDistance(Config.PLAYERSIZE));
                        }
                    }
                    lock (x)
                    {
                        foreach (Enemy enemy in enemies)
                        {
                            graphic.FillRectangle(new SolidBrush(enemy.getColor()), GetDistance(enemy.position.x) - GetDistance(Config.ENEMYSIZE / 2), GetDistance(enemy.position.y) - GetDistance(Config.ENEMYSIZE / 2), GetDistance(Config.ENEMYSIZE), GetDistance(Config.ENEMYSIZE));
                        }
                    }
                    lock (bulletLock)
                    {
                        if (bullets != null)
                            foreach (Bullet bullet in bullets)
                            {
                                graphic.FillRectangle(new SolidBrush(bullet.color), GetDistance(bullet.position.x) - GetDistance(Config.BULLETWIDTH / 2), GetDistance(bullet.position.y) - GetDistance(Config.BULLETWIDTH / 2), GetDistance(Config.BULLETWIDTH), GetDistance(Config.BULLETWIDTH));
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
