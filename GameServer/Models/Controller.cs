using System;
using SuperWebSocket;
using Newtonsoft.Json;
using GameServer.Messages;

namespace GameServer.Models
{
    class Controller
    {
        private static WebSocketServer wsServer;

        public Controller()
        {
            wsServer = new WebSocketServer();
            int port = 8088;
            wsServer.Setup(port);
            wsServer.NewSessionConnected += WsServer_NewSessionConnected;
            wsServer.NewMessageReceived += WsServer_NewMessageReceived;
            wsServer.NewDataReceived += WsServer_NewDataReceived;
            wsServer.SessionClosed += WsServer_SessionClosed;
            wsServer.Start();
            Console.WriteLine("Server is running on port " + port + ". Press ENTER to exit....");
            Console.ReadKey();
        }

        private static void WsServer_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            Console.WriteLine("SessionClosed");
        }

        private static void WsServer_NewDataReceived(WebSocketSession session, byte[] value)
        {
            Console.WriteLine("NewDataReceived");
            session.Send("NewDataReceived");
        }

        private static void WsServer_NewMessageReceived(WebSocketSession session, string value)
        {

            SocketMessage bsObj = JsonConvert.DeserializeObject<SocketMessage>(value);

            Console.WriteLine("NewMessageReceived: " + value);
            Console.WriteLine("type: " + bsObj.type);
            switch(bsObj.type){
                case MoveMessage.TYPE:

                    break;
            }
            Console.WriteLine("data: " + bsObj.data);
            if (value == "Hello server")
            {
                session.Send("Hello client");
            }
        }

        private static void WsServer_NewSessionConnected(WebSocketSession session)
        {
            Console.WriteLine("NewSessionConnected");
        }


    }
}
