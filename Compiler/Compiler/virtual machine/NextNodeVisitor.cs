using Compiler.action_nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.virtual_machine
{
    public class NextNodeVisitor : INodeVisitor
    {
        private VirtualMachine VM { get; set; }
        public ActionNode NextNode { get; private set; }

        public NextNodeVisitor(VirtualMachine vm)
        {
            VM = vm;
        }

        public void Visit(ConditionalJump visited)
        {
            if (VM.ReturnValue == "true")
                NextNode = visited.OnTrueJumpToNode;
            else
                NextNode = visited.OnFalseJumpToNode;
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
