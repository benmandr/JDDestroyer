using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Geometry
{
    class Bounds
    {
        public Position nw = null;
        public Position se = null;

        public Bounds()
        {

        }

        public Bounds(Position nw, Position se)
        {
            this.nw = nw;
            this.se = se;
        }

        public static Bounds MainSquare()
        {
            return new Bounds(new Position(0, 0), new Position(100, 100));
        }

        public static Bounds InnerSquare()
        {
            double start = (100 - Config.INNERSQUARESIZE) / 2;         
            return new Bounds(new Position(start, start), new Position(start + Config.INNERSQUARESIZE, start + Config.INNERSQUARESIZE));
        }

        public Bounds(Position enemyPos, double width)
        {
            nw = new Position(enemyPos.x - width / 2, enemyPos.y - width / 2);
            se = new Position(enemyPos.x + width / 2, enemyPos.y + width / 2);
        }

        public bool Intersects(Bounds bounds)
        {
            if (nw.x > bounds.se.x || bounds.nw.x > se.x)
                return false;
            if (nw.y > bounds.se.y || bounds.nw.y > se.y)
                return false;

            return true;
        }

        
        public bool InBounds(Bounds bounds)
        {
            if (nw.x > bounds.nw.x || nw.y > bounds.nw.y || se.x < bounds.se.x || se.y < bounds.se.y)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return this.nw.ToString() + this.se.ToString();
        }
    }
}
