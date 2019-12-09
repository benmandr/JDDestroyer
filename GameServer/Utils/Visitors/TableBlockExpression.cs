using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Utils.Visitors
{
    class TableBlockExpression : IVisitorExpression
    {
        public List<RowExpression> rows = new List<RowExpression>();

        public TableBlockExpression(List<RowExpression> rows)
        {
            this.rows = rows;
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
