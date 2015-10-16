using Compiler.virtual_machine;

namespace Compiler.action_nodes
{
    public abstract class ActionNode
    {
        public ActionNode Next { get; set; }

        public virtual void Execute(VirtualMachine vm) { }
        public abstract void Accept(INodeVisitor visitor);
    }
}
