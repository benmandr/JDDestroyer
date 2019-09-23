using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Models;

namespace GameServer.Messages
{
    class MoveMessage
    {
        public const int TYPE = 1;

        public Position position { get; set; }


    }
}
