using System;
using System.Collections.Generic;
using System.Text;
using SuperWebSocket;
using Newtonsoft.Json;

namespace GameServer.Models
{
    class Player
    {
        public string name { get; set; }
        public WebSocketSession session { get; set; }

        public Player(string name, WebSocketSession session)
        {
            this.name = name;
            this.session = session;
        }

        public void sendMessage(string message)
        {
            session.Send(message);
        }
    }
}
