using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Utils.Visitors
{
    class RowExpression : IVisitorExpression
    {
        public TextExpression first { get; private set; }
        public TextExpression second { get; private set; }
        public RowExpression(TextExpression first, TextExpression second)
        {
            this.first = first;
            this.second = second;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
