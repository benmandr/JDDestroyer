using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models
{
    class GamePlayer
    {
        public Player player { get; set; }
        public Position position { get; set; }
        public Game game { get; set; }

        public GamePlayer(Player player)
        {
            this.player = player;
        }

        public void sendMessage(string message)
        {
            player.sendMessage(message);
        }
        public bool Equals(GamePlayer obj)
        {
            if(obj == null)
            {
                return false;
            }
            return player.session.SessionID == obj.player.session.SessionID;
        }
    }
}
