using Compiler.virtual_machine;

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
