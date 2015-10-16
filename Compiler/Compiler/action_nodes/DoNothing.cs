using Compiler.virtual_machine;

namespace Compiler.action_nodes
{
    public class DoNothing: ActionNode
    {
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
