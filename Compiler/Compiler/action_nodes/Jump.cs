using Compiler.virtual_machine;

namespace Compiler.action_nodes
{
    public class Jump : ActionNode
    {
        public ActionNode JumpToNode { get; set; }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
