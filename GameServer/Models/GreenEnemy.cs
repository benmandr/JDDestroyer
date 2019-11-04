using System;
using System.Threading;
using System.Drawing;
using GameServer.Geometry;
namespace GameServer.Models
{
    public class GreenEnemy : Enemy
    {
        public const int TYPE = 2;

        public override int getType => TYPE;

        public override Color getColor()
        {
            return Color.Green;
        }

        public GreenEnemy(Position position) : base(position) { }
    }
}
