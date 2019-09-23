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
        private Game game = null;
        private int playerCount = 0;
        public Controller()
        {
            wsServer = new WebSocketServer();
            int port = 8088;
            wsServer.Setup(port);
            wsServer.NewSessionConnected += CreatePlayer;
            wsServer.NewMessageReceived += ParseMessage;
            wsServer.SessionClosed += Disconnect;
            wsServer.Start();
            Console.WriteLine("Server is running on port " + port + ". Press ENTER to exit....");
            Console.ReadKey();
        }

        private void Disconnect(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            GamePlayer gamePlayer = getGamePlayer(session);
            if (gamePlayer != null)
            {
                gamePlayer.game.removePlayer(gamePlayer);
                if (gamePlayer.game.isEmpty())
                {
                    gamePlayer.game = null;
                    game = null;
                }
            }
            sessionPlayers[session.SessionID] = null;
        }

        private void ParseMessage(WebSocketSession session, string value)
        {
            SocketMessage bsObj = JsonConvert.DeserializeObject<SocketMessage>(value);

            switch (bsObj.type)
            {
                case MoveMessage.TYPE:

                    Position position = JsonConvert.DeserializeObject<Position>(bsObj.data.ToString());

                    MovePlayer(session, position);
                    break;
            }
        }

        private void CreatePlayer(WebSocketSession session)
        {
            GamePlayer gamePlayer = new GamePlayer(new Player("Destroyer" + playerCount++, session));
            gamePlayer.sendMessage("Player created. Name:" + gamePlayer.player.name);
            if (game == null)
            {
                game = new Game(gamePlayer);
                game.sendMessage("Game created");
            }
            else
            {
                game.addPlayer(gamePlayer);
                gamePlayer.sendMessage("Game joined");
            }
            sessionPlayers[session.SessionID] = gamePlayer;
        }

        private void MovePlayer(WebSocketSession session, Position position)
        {
            GamePlayer gamePlayer = getGamePlayer(session);
            if (gamePlayer != null)
            {
                gamePlayer.game.sendMessage("Player `" + gamePlayer.player.name + "` moved from [" + gamePlayer.position.x + "," + gamePlayer.position.y + "] to [" + position.x + "," + position.y + "]", gamePlayer);
                gamePlayer.position = position;
            }
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
