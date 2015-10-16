using Compiler.virtual_machine;

namespace Compiler.action_nodes
{
    public class ConditionalJump : ActionNode
    {
        public ConditionalJump(ActionNode onTrueJumpNode, ActionNode onFalseJumpNode)
        {
            OnTrueJumpToNode = onTrueJumpNode;
            OnFalseJumpToNode = onFalseJumpNode;
        }

        private ActionNode _onTrueJumpToNode;
        public ActionNode OnTrueJumpToNode
        {
            get { return _onTrueJumpToNode; }
            private set { _onTrueJumpToNode = value; }
        }

        private ActionNode _onFalseJumpToNode;
        public ActionNode OnFalseJumpToNode
        {
            get { return _onFalseJumpToNode; }
            private set { _onFalseJumpToNode = value; }
        }
        
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
