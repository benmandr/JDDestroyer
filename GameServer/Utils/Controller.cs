﻿using System;
using SuperWebSocket;
using Newtonsoft.Json;
using GameServer.Messages;
using System.Collections.Generic;
using System.Threading;
using GameServer.Utils.Visitors;

namespace GameServer.Models
{
    class Controller
    {
        private WebSocketServer wsServer;
        private Dictionary<string, GamePlayer> sessionPlayers = new Dictionary<string, GamePlayer>();
        private GameFacade game = null;
        private int playerCount = 0;
        private static Controller instance = new Controller();


        public void initiateConnection()
        {
            wsServer = new WebSocketServer();
            int port = Config.SERVER_PORT;
            wsServer.Setup(port);
            wsServer.NewSessionConnected += CreatePlayer;
            wsServer.NewMessageReceived += ParseMessage;
            wsServer.SessionClosed += Disconnect;
            wsServer.Start();
            Console.WriteLine("Server is running on port " + port + ". Press ENTER to exit....");
            StartInterpreter();
            Console.ReadKey();
        }

        public void StartInterpreter()
        {
            new Thread(() =>
            {
                while (true)
                {
                    string input = Console.ReadLine();
                    ExpressionFactory expressionFactory = ExpressionFactory.getInstance();
                    Expression addRedEnemy = expressionFactory.getExpression(RedEnemy.TYPE);
                    Expression addBlueEnemy = expressionFactory.getExpression(BlueEnemy.TYPE);
                    Expression addGreenEnemy = expressionFactory.getExpression(GreenEnemy.TYPE);
                    Expression viewScore = new AndExpression(new TerminalExpression("view"), new TerminalExpression("score"));

                    if (addRedEnemy.Interpret(input))
                    {
                        addNewEnemyViaInterpreter(RedEnemy.TYPE);
                    }
                    else if (addBlueEnemy.Interpret(input))
                    {
                        addNewEnemyViaInterpreter(BlueEnemy.TYPE);
                    }
                    else if (addGreenEnemy.Interpret(input))
                    {
                        addNewEnemyViaInterpreter(GreenEnemy.TYPE);
                    }
                    else if (viewScore.Interpret(input))
                    {
                        writeScores();
                    }
                    else
                    {
                        Console.WriteLine("Expression fail.");
                    }
                }
            }).Start();
        }

        public void writeScores()
        {
            RowExpression title = new RowExpression(new TextExpression("Player name"), new TextExpression("Score"));

            List <RowExpression> rows = new List<RowExpression>();
            int cnt = 0;
            foreach(GamePlayer player in game.gamePlayers.getPlayers())
            {
                rows.Add(new RowExpression(new TextExpression("Player" + cnt++), new TextExpression(player.score.ToString())));
            }

            List<TableBlockExpression> blocks = new List<TableBlockExpression>();
            blocks.Add(new TableBlockExpression(rows));

            TableExpression table = new TableExpression(title, blocks);

            ScoreVisitor visitor = new ScoreVisitor(new System.Text.StringBuilder());
            visitor.Visit(table);

            Console.WriteLine(visitor);

        }

        public void addNewEnemyViaInterpreter(int type)
        {
            game.mover.addItem(new EnemyAdapter(EnemyFactory.getInstance().getEnemy(type)));
        }

        public static Controller getInstance()
        {
            return instance;
        }

        private void Disconnect(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            GamePlayer gamePlayer = getGamePlayer(session);
            //Console.WriteLine("player " + gamePlayer.player.name + " disconnected");
            if (gamePlayer != null)
            {
                gamePlayer.game.gamePlayers.removePlayer(gamePlayer);
                game.mover.removeObserver(gamePlayer);
                if (gamePlayer.game.gamePlayers.isEmpty())
                {
                    gamePlayer.game = null;
                    game = null;
                } else
                {
                    game.mover.observers.ForEach(x => x.PlayerListChange(game));
                }
            }
            sessionPlayers[session.SessionID] = null;
        }

        private void ParseMessage(WebSocketSession session, string value)
        {
            //Console.WriteLine("Geting");
            //Console.WriteLine(value);

            SocketMessage bsObj = JsonConvert.DeserializeObject<SocketMessage>(value);
            GamePlayer gamePlayer = getGamePlayer(session);

            switch (bsObj.type)
            {
                case MoveRightMessage.TYPE:
                    if (gamePlayer != null)
                    {
                        if (gamePlayer.MoveRight())
                        {
                            gamePlayer.PositionChanged();
                        }
                    }
                    break;
                case ShootMessage.TYPE:
                    if (gamePlayer != null)
                    {
                        gamePlayer.Shoot();
                    }
                    break;
                case MoveLeftMessage.TYPE:
                    gamePlayer = getGamePlayer(session);
                    if (gamePlayer != null)
                    {
                        if (gamePlayer.MoveLeft())
                        {
                            gamePlayer.PositionChanged();
                        }
                    }
                    break;
            }
        }

        private void CreatePlayer(WebSocketSession session)
        {
            GamePlayer gamePlayer = new GamePlayer(new Player("Destroyer" + playerCount++, session));

            if (game == null)
            {
                game = new GameFacade();
                game.StartGame();
               // Console.WriteLine("Game created");
            }


            game.AddPlayer(gamePlayer);

            PlayerDataMessage playerData = new PlayerDataMessage();
            playerData.position = gamePlayer.position;
            playerData.name = gamePlayer.player.name;
            playerData.id = gamePlayer.player.id;
            playerData.color = gamePlayer.color;

            SocketMessage playerDataMessage = new SocketMessage();
            playerDataMessage.type = PlayerDataMessage.TYPE;
            playerDataMessage.data = JsonConvert.SerializeObject(playerData);
            gamePlayer.sendMessage(JsonConvert.SerializeObject(playerDataMessage));

            // Console.WriteLine("Game joined");

            sessionPlayers[session.SessionID] = gamePlayer;
        }

        private GamePlayer getGamePlayer(WebSocketSession session)
        {
            GamePlayer gamePlayer;
            if (sessionPlayers.TryGetValue(session.SessionID, out gamePlayer))
            {
                return gamePlayer;
            }
            return null;
        }
    }
}
