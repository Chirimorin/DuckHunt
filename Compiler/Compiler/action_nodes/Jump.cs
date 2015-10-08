using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.action_nodes
{
    public class Jump : ActionNode
    {
        private ActionNode _jumpToNode;
        public ActionNode JumpToNode
        {
            get { return _jumpToNode; }
            set { _jumpToNode = value; }
        }

        public override void Accept(NodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
