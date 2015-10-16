using Compiler.action_nodes;

namespace Compiler.virtual_machine
{
    public class NextNodeVisitor : INodeVisitor
    {
        private VirtualMachine Vm { get; }
        public ActionNode NextNode { get; private set; }

        public NextNodeVisitor(VirtualMachine vm)
        {
            Vm = vm;
        }

        public void Visit(ConditionalJump visited)
        {
            NextNode = Vm.ReturnValue == "True" ? visited.OnTrueJumpToNode : visited.OnFalseJumpToNode;
        }

        public void Visit(DirectFunctionCall visited)
        {
            NextNode = visited.Next;
        }

        public void Visit(DoNothing visited)
        {
            NextNode = visited.Next;
        }

        public void Visit(FunctionCall visited)
        {
            NextNode = visited.Next;
        }

        public void Visit(Jump visited)
        {
            NextNode = visited.JumpToNode;
        }
    }
}
