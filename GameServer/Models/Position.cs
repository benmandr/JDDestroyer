using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Models
{
    class Position
    {
        public float x { get; set; }
        public float y { get; set; }

        public Position(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void addX(float x)
        {
            this.x += x;
        }

        public void addY(float y)
        {
            this.y += y;
        }
    }
}
