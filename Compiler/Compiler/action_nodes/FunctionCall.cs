using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.action_nodes
{
    public class FunctionCall : AbstractFunctionCall
    {
        public override void Accept(NodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
