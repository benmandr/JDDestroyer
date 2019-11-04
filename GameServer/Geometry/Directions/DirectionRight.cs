using GameServer.Geometry;

namespace GameServer.Directions
{
    public class DirectionRight : Direction
    {
        public override void MoveForward(Position position, double distance)
        {
            position.addX(distance);
            position.subtractX(distance);
        }

        public override void MoveBack(Position position, double distance)
        {
            position.subtractX(distance);
        }
    }
}
