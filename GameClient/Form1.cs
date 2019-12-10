using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;
using System.Windows.Forms;
using GameServer;
using GameServer.Models;
using GameServer.Messages;
using WebSocketSharp;
using GameClient.GraphicItems;

namespace GameClient
{
    public partial class Form1 : Form
    {
        Bitmap BackBuffer;
        Bitmap FrontBuffer;
        Bitmap TopBuffer;

        Player currentPlayer = null;
        GameFacade currentGame = null;
        List<Enemy> enemies = new List<Enemy>();

        GoldenTooth goldenTooth = null;

        private long ping = 0;

        WebSocket webSocket;
        private readonly object x = new object();
        private readonly object bulletLock = new object();
        private readonly object playerLock = new object();
        private bool showScoreTable = false;

        List<Bullet> bullets = new List<Bullet>();
        List<GraphicItem> graphicItems = new List<GraphicItem>();

        private static int angle = 0;

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
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);
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
            ping = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - bsObj.sendTime;
            switch (bsObj.type)
            {
                case EnemiesDataMessage.TYPE:
                    EnemiesDataMessage enemiesData = JsonConvert.DeserializeObject<EnemiesDataMessage>(bsObj.data);
                    lock (x)
                    {
                        enemies = new List<Enemy>();
                        if (enemiesData.enemiesList != null)
                            foreach (EnemyDummy dummy in enemiesData.enemiesList)
                                enemies.Add(Enemy.createFromDummy(dummy));
                    }
                    break;

                case PositionChangedMessage.TYPE:
                    PositionChangedMessage positionChange = JsonConvert.DeserializeObject<PositionChangedMessage>(bsObj.data);

                    GamePlayer gamePlayer = currentGame.gamePlayers.getPlayer(positionChange.playerId);
                    gamePlayer.position = positionChange.position;
                    break;

                case PlayerScoreMessage.TYPE:
                    PlayerScoreMessage data = JsonConvert.DeserializeObject<PlayerScoreMessage>(bsObj.data);

                    currentGame.gamePlayers = data.players;

                    break;
                case BulletsDataReceiveMessage.TYPE:
                    BulletsDataReceiveMessage bulletsData = JsonConvert.DeserializeObject<BulletsDataReceiveMessage>(bsObj.data);
                    lock (bulletLock)
                    {
                        bullets = new List<Bullet>();
                        if (bulletsData.bulletList != null)
                            bullets = bulletsData.bulletList;
                    }
                    break;
                case PlayerAngleMessage.TYPE:
                    PlayerAngleMessage angleData = JsonConvert.DeserializeObject<PlayerAngleMessage>(bsObj.data);
                    angle = angleData.angle;
                    break;

                case GoldenToothMessage.TYPE:
                    GoldenToothMessage goldenToothData = JsonConvert.DeserializeObject<GoldenToothMessage>(bsObj.data);
                    if (goldenTooth == null)
                        goldenTooth = new GoldenTooth();
                    goldenTooth.position = goldenToothData.position;
                    break;
                case PlayerDataMessage.TYPE:
                    PlayerDataMessage playerData = JsonConvert.DeserializeObject<PlayerDataMessage>(bsObj.data);
                    if (currentPlayer == null)
                    {
                        currentPlayer = new Player(playerData.id, playerData.name);
                    }
                    break;
                case GameDataMessage.TYPE:
                    GameFacade game = JsonConvert.DeserializeObject<GameFacade>(bsObj.data);
                    if (currentGame == null)
                        currentGame = new GameFacade();
                    currentGame.name = game.name;
                    lock (playerLock)
                    {
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
                    }
                    break;
            }
        }


        void SendMessage(string data)
        {
            if (webSocket != null)
                webSocket.Send(data);
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                SocketMessage message = new SocketMessage
                {
                    type = MoveLeftMessage.TYPE
                };
                SendMessage(JsonConvert.SerializeObject(message));
            }
            else if (e.KeyCode == Keys.Right)
            {
                SocketMessage message = new SocketMessage
                {
                    type = MoveRightMessage.TYPE
                };
                SendMessage(JsonConvert.SerializeObject(message));
            }
            else if (e.KeyCode == Keys.Space)
            {
                SocketMessage message = new SocketMessage
                {
                    type = ShootMessage.TYPE
                };
                SendMessage(JsonConvert.SerializeObject(message));
            }
            else if (e.KeyCode == Keys.Tab)
            {
                this.showScoreTable = true;
            }
        }

        void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
                this.showScoreTable = false;
        }

        void FormPaint(object sender, PaintEventArgs e)
        {
            if (BackBuffer != null)
            {
                e.Graphics.DrawImageUnscaled(BackBuffer, Point.Empty);
                e.Graphics.DrawImageUnscaled(FrontBuffer, Point.Empty);
                e.Graphics.DrawImageUnscaled(TopBuffer, Point.Empty);
            }
        }

        void Init(object sender, EventArgs ev)
        {
            if (BackBuffer != null)
            {
                BackBuffer.Dispose();
                FrontBuffer.Dispose();
                TopBuffer.Dispose();
            }
            Proportion.windowWidth = ClientSize.Width;
            BackBuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
            FrontBuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
            TopBuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
            (new MainWindow(BackBuffer)).Draw();
            (new CornerSquares(BackBuffer)).Draw();
            (new MiddleSquare(BackBuffer)).Draw();
        }

        void Tick(object sender, EventArgs e)
        {
            if (currentGame == null)
                return;

            graphicItems.Clear();
            BlockList blocks = new BlockList();

            lock (playerLock)
                foreach (GamePlayer player in currentGame.gamePlayers.getPlayers())
                    blocks.Add(new PlayerBlock(FrontBuffer, player.color, player.position));

            lock (x)
                foreach (Enemy enemy in enemies)
                    blocks.Add(new EnemyBlock(FrontBuffer, enemy.getColor(), enemy.position));

            lock (bulletLock)
                foreach (Bullet bullet in bullets)
                    blocks.Add(new BulletBlock(FrontBuffer, bullet.color, bullet.position));
            if (goldenTooth != null)
                blocks.Add(new GoldenToothBlock(FrontBuffer, goldenTooth.position));

            graphicItems.Add(blocks);
            graphicItems.Add(new PlayerScore(TopBuffer));
            if (showScoreTable)
                graphicItems.Add(new ScoreTable(TopBuffer, currentGame.gamePlayers));

            if (BackBuffer != null)
            {
                using (var graphic = Graphics.FromImage(FrontBuffer))
                {
                    graphic.Clear(Color.Transparent);
                }
                using (var graphic = Graphics.FromImage(TopBuffer))
                {
                    graphic.Clear(Color.Transparent);
                }
            }
            Invalidate();
            graphicItems.ForEach(x => x.Draw());
            if (FrontBuffer != null)
            {
                FrontBuffer = RotateImage(FrontBuffer, angle);
            }

        }

        public static Bitmap RotateImage(Bitmap b, float angle)
        {
            if (angle == 0)
                return b;
            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            //make a graphics object from the empty bitmap
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                //move rotation point to center of image
                g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
                //rotate
                g.RotateTransform(angle);
                //move image back
                g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
                //draw passed in image onto graphics object
                g.DrawImage(b, new Point(0, 0));
            }
            return returnBitmap;
        }
    }
}
