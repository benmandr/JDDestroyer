﻿using GameServer.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public interface IGoldenTooth
    {
        Position position { get; set; }
        Position direction { get; set; }

        void changeDirection();

        bool Move();
    }
}
