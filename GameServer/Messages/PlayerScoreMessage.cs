using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Messages
{
    public class PlayerScoreMessage
    {
        public const int TYPE = 11;
        public GamePlayers players { get; set; }
        public PlayerScoreMessage()
        {
        }

        public PlayerScoreMessage(GamePlayers players)
        {
            this.players = players;
        }
    }
}
