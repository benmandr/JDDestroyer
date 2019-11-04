using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Geometry;

namespace GameServer.Directions
{
    public class DirectionTop : Direction
    {
        public override void MoveForward(Position position, double distance)
        {
            position.subtractY(distance);
        }

        public override void MoveBack(Position position, double distance)
        {
            position.addY(distance);
        }
    }
}
