using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Models
{
    public class Position
    {
        public double x { get; set; }
        public double y { get; set; }

        public Position(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Position addX(double x)
        {
            this.x += x;
            return this;
        }

        public Position addY(double y)
        {
            this.y += y;
            return this;
        }

        public Position subtractX(double x)
        {
            this.x -= x;
            return this;
        }

        public Position subtractY(double y)
        {
            this.y -= y;
            return this;
        }
    }
}
