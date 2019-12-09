using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Utils.Visitors
{
    class TableExpression : IVisitorExpression
    {
        public RowExpression title { get; private set; }
        public List<TableBlockExpression> blocks = new List<TableBlockExpression>();
        public TableExpression(RowExpression title, List<TableBlockExpression> blocks)
        {
            this.blocks = blocks;
            this.title = title;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
