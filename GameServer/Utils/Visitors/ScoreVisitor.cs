using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Utils.Visitors
{
    class ScoreVisitor : IVisitor
    {
        StringBuilder sb;
        public ScoreVisitor(StringBuilder sb)
        {
            this.sb = sb;
        }

        const int TABLE_SIZE = 69;
        const int TEXT_EXPRESSION_SIZE = (TABLE_SIZE-3) / 2;
        public void Visit(TableBlockExpression block)
        {
            sb.Append(new string('-', TABLE_SIZE));
            sb.Append('\n');
            foreach (RowExpression row in block.rows)
            {
                Visit(row);
            }
            sb.Append(new string('-', TABLE_SIZE));
            sb.Append('\n');
        }

        public void Visit(RowExpression row)
        {
            sb.Append('|');
            Visit(row.first);
            sb.Append('|');
            Visit(row.second);
            sb.Append('|');
            sb.Append('\n');
        }

        public void Visit(TextExpression row)
        {
            string text = row.text.PadRight(TEXT_EXPRESSION_SIZE);
            sb.Append(text);
        }

        public void Visit(TableExpression table)
        {
            Visit(table.title);
            foreach (TableBlockExpression block in table.blocks)
            {
                Visit(block);
            }
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
