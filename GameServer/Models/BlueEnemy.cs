using System;
using System.Threading;
using System.Drawing;
using GameServer.Geometry;

namespace GameServer.Models
{
    public class BlueEnemy : Enemy
    {

        public const int TYPE = 3;
        public override int getType => TYPE;

        public override Color getColor()
        {
            return Color.Blue;
        }

        public BlueEnemy(Position position) : base(position) { }

        public static Expression AddEnemyExpression()
        {
            return new AndExpression(new TerminalExpression("add enemy"), new TerminalExpression("blue"));
        }
    }
}
