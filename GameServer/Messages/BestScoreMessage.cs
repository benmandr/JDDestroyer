using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Messages
{
    public class BestScoreMessage
    {
        public const int TYPE = 13;
        public long score { get; set; }
        public BestScoreMessage()
        {
        }

        public BestScoreMessage(long score)
        {
            this.score = score;
        }
    }
}
