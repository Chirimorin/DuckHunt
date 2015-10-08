using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.action_nodes
{
    public class ConditionalJump : ActionNode
    {
        private ActionNode _onTrueJumpToNode;
        public ActionNode OnTrueJumpToNode
        {
            get { return _onTrueJumpToNode; }
            set { _onTrueJumpToNode = value; }
        }

        private ActionNode _onFalseJumpToNode;
        public ActionNode OnFalseJumpToNode
        {
            get { return _onFalseJumpToNode; }
            set { _onFalseJumpToNode = value; }
        }
        
        public override void Accept(NodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
