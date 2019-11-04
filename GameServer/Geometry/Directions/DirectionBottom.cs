using GameServer.Geometry;

namespace GameServer.Directions
{
    public class DirectionBottom : Direction
    {
        public override void MoveForward(Position position, double distance)
        {
            position.addY(distance);
        }

        public override void MoveBack(Position position, double distance)
        {
            position.subtractY(distance);
        }
    }
}
