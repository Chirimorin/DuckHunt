using Compiler.exceptions;

namespace Compiler.virtual_machine.Commands
{
    public class ReturnToVariableCommand : ICommand
    {
        public void Execute(string[] arguments, VirtualMachine vm)
        {
            if (arguments == null || arguments.Length != 1)
            {
                throw new RuntimeException("Incorrect arguments for ReturnToVariableCommand");
            }

            vm.Variables[arguments[0]] = int.Parse(vm.ReturnValue);
            vm.ReturnValue = "";
        }
    }
}
