using Compiler.exceptions;

namespace Compiler.virtual_machine.Commands
{
    public class VariableToReturnCommand : ICommand
    {
        public void Execute(string[] arguments, VirtualMachine vm)
        {
            if (arguments == null || arguments.Length != 1)
            {
                throw new RuntimeException("Incorrect arguments for VariableToReturnCommand");
            }

            int result;
            if (vm.Variables.TryGetValue(arguments[0], out result))
            {
                vm.ReturnValue = result.ToString();
            }
            else
                throw new RuntimeException("Variable " + arguments[0] + " does not exist");
        }
    }
}
