using Compiler.exceptions;

namespace Compiler.virtual_machine.Commands
{
    public abstract class OperatorCommand : ICommand
    {
        public void Execute(string[] arguments, VirtualMachine vm)
        {
            if (arguments == null || arguments.Length != 2)
            {
                throw new RuntimeException("Incorrect amount of arguments for OperatorCommand");
            }

            int value1 = int.Parse(vm.Variables[arguments[0]]),
                value2 = int.Parse(vm.Variables[arguments[1]]);

            vm.ReturnValue = Calculate(value1, value2).ToString();
        }

        protected abstract int Calculate(int left, int right);
    }

    public class PlusCommand : OperatorCommand
    {
        protected override int Calculate(int left, int right)
        {
            return left + right;
        }
    }

    public class MinusCommand : OperatorCommand
    {
        protected override int Calculate(int left, int right)
        {
            return left - right;
        }
    }
}
