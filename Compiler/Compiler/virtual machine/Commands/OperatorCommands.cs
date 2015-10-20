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

            vm.ReturnValue = Calculate(vm.Variables[arguments[0]], vm.Variables[arguments[1]]).ToString();
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

    public class MultiplyCommand : OperatorCommand
    {
        protected override int Calculate(int left, int right)
        {
            return left*right;
        }
    }

    public class DivideCommand : OperatorCommand
    {
        protected override int Calculate(int left, int right)
        {
            return left/right;
        }
    }
}
