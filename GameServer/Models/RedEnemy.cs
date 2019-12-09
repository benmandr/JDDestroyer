using System;
using System.Threading;
using System.Drawing;
using GameServer.Geometry;

namespace GameServer.Models
{
    public class RedEnemy : Enemy
    {

        public const int TYPE = 1;
        public override int getType => TYPE;

        public override Color getColor()
        {
            return Color.Red;
        }

        public RedEnemy(Position position) : base(position) { }

        public static Expression AddEnemyExpression()
        {
            return new AndExpression(new TerminalExpression("add enemy"), new TerminalExpression("red"));
        }
    }
}
