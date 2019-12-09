using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Utils.Visitors
{
    interface IVisitor
    {
        void Visit(TableBlockExpression row);
        void Visit(RowExpression row);
        void Visit(TextExpression row);
        void Visit(TableExpression row);
    }
}
