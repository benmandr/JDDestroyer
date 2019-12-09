using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models
{
    class ExpressionFactory
    {
        private static ExpressionFactory factory = new ExpressionFactory();

        public static ExpressionFactory getInstance()
        {
            return factory;
        }

        public Expression getExpression(int enemyType)
        {
            switch (enemyType)
            {
                case RedEnemy.TYPE:
                    return new AndExpression(new TerminalExpression("add enemy"), new TerminalExpression("red"));
                case BlueEnemy.TYPE:
                    return new AndExpression(new TerminalExpression("add enemy"), new TerminalExpression("blue"));
                case GreenEnemy.TYPE:
                    return new AndExpression(new TerminalExpression("add enemy"), new TerminalExpression("green"));
            }
            return null;
        }
    }
}
