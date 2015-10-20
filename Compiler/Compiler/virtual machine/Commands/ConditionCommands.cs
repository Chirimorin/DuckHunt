using Compiler.exceptions;

namespace Compiler.virtual_machine.Commands
{
    public abstract class ConditionCommand : ICommand
    {
        public void Execute(string[] arguments, VirtualMachine vm)
        {
            if (arguments == null || arguments.Length != 2)
            {
                throw new RuntimeException("Incorrect arguments for Condition");
            }

            vm.ReturnValue = Calculate(vm.Variables[arguments[0]], vm.Variables[arguments[1]]).ToString();
        }

        protected abstract bool Calculate(int left, int right);
    }

    public class EqualsCommand : ConditionCommand
    {
        protected override bool Calculate(int left, int right)
        {
            return left == right;
        }
    }

    public class NotEqualsCommand : ConditionCommand
    {
        protected override bool Calculate(int left, int right)
        {
            return left != right;
        }
    }

    public class GreaterEqualsCommand : ConditionCommand
    {
        protected override bool Calculate(int left, int right)
        {
            return left >= right;
        }
    }

    public class GreaterThanCommand : ConditionCommand
    {
        protected override bool Calculate(int left, int right)
        {
            return left > right;
        }
    }

    public class SmallerEqualsCommand : ConditionCommand
    {
        protected override bool Calculate(int left, int right)
        {
            return left <= right;
        }
    }

    public class SmallerThanCommand : ConditionCommand
    {
        protected override bool Calculate(int left, int right)
        {
            return left < right;
        }
    }
}
