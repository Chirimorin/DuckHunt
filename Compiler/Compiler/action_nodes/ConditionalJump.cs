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

        public ActionNode OnTrueJumpToNode { get; private set; }

        public ActionNode OnFalseJumpToNode { get; private set; }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
