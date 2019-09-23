using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models
{
    class SocketMessage
    {
        public int type { get; set; }

        public string data { get; set; }
    }
}
