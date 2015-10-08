using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.action_nodes
{
    public class ConditionalJump : ActionNode
    {
        private ActionNode _onTrueJumpTo;
        public ActionNode OnTrueJumpTo
        {
            get { return _onTrueJumpTo; }
            set { _onTrueJumpTo = value; }
        }

        private ActionNode _onFalseJumpTo;
        public ActionNode OnFalseJumpTo
        {
            get { return _onFalseJumpTo; }
            set { _onFalseJumpTo = value; }
        }
        
        public override void Accept(NodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
