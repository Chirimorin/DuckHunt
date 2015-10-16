using Compiler.virtual_machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.action_nodes
{
    public abstract class AbstractFunctionCall : ActionNode
    {
        public string ActionName { get; protected set; }
        public string[] Parameters { get; protected set; }

        public override void Execute(VirtualMachine vm)
        {
            vm.GetCommand(ActionName).Execute(Parameters, vm);
        }
    }
}
