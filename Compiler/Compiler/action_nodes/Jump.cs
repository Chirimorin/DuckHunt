using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.action_nodes
{
    public class Jump : ActionNode
    {
        private ActionNode _jumpTo;
        public ActionNode JumpTo
        {
            get { return _jumpTo; }
            set { _jumpTo = value; }
        }

        public override void Accept(NodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
