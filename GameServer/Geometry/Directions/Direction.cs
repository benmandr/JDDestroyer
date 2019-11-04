using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Geometry;

namespace GameServer.Directions
{
    public abstract class Direction
    {
        abstract public void MoveForward(Position position, double distance);
        abstract public void MoveBack(Position position, double distance);
    }
}
