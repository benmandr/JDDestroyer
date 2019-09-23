using System;
using System.Collections.Generic;
using System.Text;
using SuperWebSocket;
using Newtonsoft.Json;

namespace GameServer.Models
{
    class Player
    {
        public static long IdIncrement = 1;

        public long id;

        public string name { get; set; }

        [JsonIgnore]
        public WebSocketSession session { get; set; }

        public Player(string name, WebSocketSession session)
        {
            id = IdIncrement++;
            this.name = name;
            this.session = session;
        }

        public void sendMessage(string message)
        {
            session.Send(message);
        }
    }
}
