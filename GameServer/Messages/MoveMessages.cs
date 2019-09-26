using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Models;

namespace GameServer.Messages
{
    public class MoveRightMessage
    {
        public const int TYPE = 1;
    }
    public class MoveLeftMessage
    {
        public const int TYPE = 2;
    }

    public class PositionChangedMessage
    {
        public const int TYPE = 3;
        public long playerId { get; set; }

        public Position position { get; set; }
    }
}
