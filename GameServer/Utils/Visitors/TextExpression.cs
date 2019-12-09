using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Utils.Visitors
{
    class TextExpression : IVisitorExpression
    {
        public string text { get; private set; }
        public TextExpression(string text)
        {
            this.text = text;
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
