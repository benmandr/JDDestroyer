using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Messages
{
    public class PlayerAngleMessage
    {
        public const int TYPE = 12;
        public int angle = 0;
        public PlayerAngleMessage()
        {
        }

        public PlayerAngleMessage(int angle)
        {
            this.angle = angle;
        }
    }
}
