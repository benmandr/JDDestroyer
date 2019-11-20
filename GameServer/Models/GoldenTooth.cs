using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer;
using GameServer.Geometry;
namespace GameServer.Models
{
    public class GoldenTooth : IMovable
    {
        public Position position { get; set; }
        public Position direction { get; set; }

        public GoldenTooth() {
        }

        public void changeDirection() {

            if(direction.x > 0)
            {
                direction = Position.DirectionTop();
            }
            else if (direction.x < 0)
            {
                direction = Position.DirectionBottom();
            }
            else if (direction.y > 0)
            {
                direction = Position.DirectionRight();
            }
            else if (direction.y < 0)
            {
                direction = Position.DirectionLeft();
            }
        }

        public bool Move()
        {
            Position posClone = (Position)position.Clone();
            posClone.add(direction);
            Bounds inner = Bounds.InnerSquare();
            if(inner.inBounds(new Bounds(posClone, Config.GOLDENTOOTHSIZE)))
            {
                position = posClone;
                return true;
            }
            changeDirection();
            return Move();
        }
    }
}

