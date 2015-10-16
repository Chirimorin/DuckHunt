using Compiler.virtual_machine;

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

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
