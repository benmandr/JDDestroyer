using System;
using SuperWebSocket;
using Newtonsoft.Json;
using GameServer.Messages;
using System.Collections.Generic;

namespace GameServer.Models
{
    class Controller
    {
        private WebSocketServer wsServer;
        private Dictionary<string, GamePlayer> sessionPlayers = new Dictionary<string, GamePlayer>();
        private GameFacade game = null;
        private int playerCount = 0;

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
            Console.ReadKey();
        }

        private static Controller instance = new Controller();
        public static Controller getInstance()
        {
            return instance;
        }

        private void Disconnect(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            GamePlayer gamePlayer = getGamePlayer(session);
            Console.WriteLine("player " + gamePlayer.player.name + " disconnected");
            if (gamePlayer != null)
            {
                gamePlayer.game.gamePlayers.removePlayer(gamePlayer);
                if (gamePlayer.game.gamePlayers.isEmpty())
                {
                    gamePlayer.game = null;
                    game = null;
                }
            }
            sessionPlayers[session.SessionID] = null;
        }

        private void ParseMessage(WebSocketSession session, string value)
        {
            Console.WriteLine("Geting");
            Console.WriteLine(value);

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
                Console.WriteLine("Game created");
            }
            game.AddPlayer(gamePlayer);
            Console.WriteLine("Game joined");

            PlayerDataMessage playerData = new PlayerDataMessage();
            playerData.position = gamePlayer.position;
            playerData.name = gamePlayer.player.name;
            playerData.id = gamePlayer.player.id;
            playerData.color = gamePlayer.color;

            SocketMessage playerDataMessage = new SocketMessage();
            playerDataMessage.type = PlayerDataMessage.TYPE;
            playerDataMessage.data = JsonConvert.SerializeObject(playerData);
            Console.WriteLine(JsonConvert.SerializeObject(playerDataMessage));
            Console.WriteLine("Sending");
            gamePlayer.sendMessage(JsonConvert.SerializeObject(playerDataMessage));

            SocketMessage gameMessage = new SocketMessage();
            gameMessage.type = GameDataMessage.TYPE;
            gameMessage.data = JsonConvert.SerializeObject(game);
            Console.WriteLine("Sending");
            Console.WriteLine(JsonConvert.SerializeObject(gameMessage));
            game.notifier.sendMessage(JsonConvert.SerializeObject(gameMessage), game.gamePlayers.getPlayers());

            foreach (Enemy enemy in game.enemySpawner.enemies)
            {
                EnemySpawnMessage messageData = new EnemySpawnMessage(enemy.position, enemy.getType);

                SocketMessage message = new SocketMessage();
                message.type = EnemySpawnMessage.TYPE;

                message.data = JsonConvert.SerializeObject(messageData);
                gamePlayer.sendMessage(JsonConvert.SerializeObject(message));
            }
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
