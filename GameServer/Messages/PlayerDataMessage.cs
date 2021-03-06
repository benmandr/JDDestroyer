﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Models;
using System.Drawing;
using GameServer.Geometry;
namespace GameServer.Messages
{
    public class PlayerDataMessage
    {
        public const int TYPE = 4;
        public int type = TYPE;
        public long id { get; set; }
        public string name { get; set; }
        public Position position { get; set; }
        public Color color { get; set; }
    }
}
