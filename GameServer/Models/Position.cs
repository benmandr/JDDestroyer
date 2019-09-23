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

        public Position addX(float x)
        {
            this.x += x;
            return this;
        }

        public Position addY(float y)
        {
            this.y += y;
            return this;
        }
    }
}
