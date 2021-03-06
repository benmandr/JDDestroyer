﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Geometry
{
    public class Position : ICloneable
    {
        public double x { get; set; }
        public double y { get; set; }


        public static bool PlayerOutOfBounds(Position position) {
            if(position.x < Config.PLAYERBOUND && (position.y < Config.PLAYERBOUND || position.y > Config.PLAYERBOUND2))
            {
                return true;
            }
            if (position.x > Config.PLAYERBOUND2 && (position.y < Config.PLAYERBOUND || position.y > Config.PLAYERBOUND2))
            {
                return true;
            }
            return false;
        }

        public static Position DirectionTop()
        {
            return new Position(0, -1);
        }

        public static Position DirectionBottom()
        {
            return new Position(0, 1);
        }

        public static Position DirectionLeft()
        {
            return new Position(-1, 0);
        }

        public static Position DirectionRight()
        {
            return new Position(1, 0);
        }

        public static Position P1InitialPosition()
        {
            return new Position(50, 100-Config.PLAYERSIZE/2);
        }

        public static Position P2InitialPosition()
        {
            return new Position(100 - Config.PLAYERSIZE / 2, 50);
        }

        public static Position P3InitialPosition()
        {
            return new Position(50, 0 + Config.PLAYERSIZE / 2);
        }

        public static Position P4InitialPosition()
        {
            return new Position(0 + Config.PLAYERSIZE / 2, 50);
        }

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

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return this.x + " : " + this.y;
        }


        public void invert()
        {
            x = 100 - x;
            y = 100 - y;
        }

        public void swap()
        {
            double j = x;
            x = y;
            y = j;
        }

        public void negative()
        {
            x *= -1;
            y *= -1;
        }

        public void multiply(double value)
        {
            x *= value;
            y *= value;
        }
        public void add(Position value)
        {
            x += value.x;
            y += value.y;
        }
    }
}
