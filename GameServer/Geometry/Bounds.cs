using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Geometry
{
    class Bounds
    {
        Position nw = null;
        Position se = null;

        public Bounds()
        {

        }

        public static Bounds MainSquare()
        {
            Bounds bounds = new Bounds();
            bounds.addPoint(new Position(0, 0));
            bounds.addPoint(new Position(100, 100));

            return bounds;
        }

        public static Bounds InnerSquare()
        {
            Bounds bounds = new Bounds();
            bounds.addPoint(new Position(Config.INNERSQUARESIZE, Config.INNERSQUARESIZE));
            bounds.addPoint(new Position(100 - Config.INNERSQUARESIZE, 100 - Config.INNERSQUARESIZE));

            return bounds;
        }

        public bool inBounds(Position position)
        {
            if(nw.x < position.x || nw.y < position.y || se.x > position.x || se.y > position.y)
            {
                return false;
            }
            return true;
        }

        public void addPoint(Position point)
        {
            if(nw == null && se == null)
            {
                nw = point;
                se = point;
                return;
            }
            if (nw.x < point.x)
            {
                nw.x = point.x;
            }
            if (nw.y < point.y)
            {
                nw.y = point.y;
            }
            if (se.x > point.x)
            {
                se.x = point.x;
            }
            if (se.y > point.y)
            {
                se.y = point.y;
            }
        }

        public override string ToString()
        {
            return this.nw.ToString() + this.se.ToString();
        }

        internal bool enemyInBounds(Position copiedPosition)
        {
            return true;
        }
    }
}
