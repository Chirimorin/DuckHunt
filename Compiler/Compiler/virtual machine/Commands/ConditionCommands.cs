using Compiler.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.virtual_machine.Commands
{
    public abstract class ConditionCommand : ICommand
    {
        public virtual void Execute(string[] arguments, VirtualMachine vm)
        {
            if (arguments == null || arguments.Length != 2)
            {
                throw new RuntimeException("Incorrect arguments for Condition");
            }
        }
    }

    public class EqualsCommand : ConditionCommand
    {
        public override void Execute(string[] arguments, VirtualMachine vm)
        {
            base.Execute(arguments, vm);
            
            vm.ReturnValue = (vm.Variables[arguments[0]] == vm.Variables[arguments[1]]).ToString();
        }
    }

    public class NotEqualsCommand : ConditionCommand
    {
        public override void Execute(string[] arguments, VirtualMachine vm)
        {
            base.Execute(arguments, vm);

            vm.ReturnValue = (vm.Variables[arguments[0]] != vm.Variables[arguments[1]]).ToString();
        }
    }

    public class GreaterEqualsCommand : ConditionCommand
    {
        public override void Execute(string[] arguments, VirtualMachine vm)
        {
            base.Execute(arguments, vm);

            int value1 = int.Parse(vm.Variables[arguments[0]]),
                value2 = int.Parse(vm.Variables[arguments[1]]);

            vm.ReturnValue = (value1 >= value2).ToString();
        }
    }

    public class GreaterThanCommand : ConditionCommand
    {
        public override void Execute(string[] arguments, VirtualMachine vm)
        {
            base.Execute(arguments, vm);

            int value1 = int.Parse(vm.Variables[arguments[0]]),
                value2 = int.Parse(vm.Variables[arguments[1]]);

            vm.ReturnValue = (value1 > value2).ToString();
        }
    }

    public class SmallerEqualsCommand : ConditionCommand
    {
        public override void Execute(string[] arguments, VirtualMachine vm)
        {
            base.Execute(arguments, vm);

            int value1 = int.Parse(vm.Variables[arguments[0]]),
                value2 = int.Parse(vm.Variables[arguments[1]]);

            vm.ReturnValue = (value1 <= value2).ToString();
        }
    }

    public class SmallerThanCommand : ConditionCommand
    {
        public override void Execute(string[] arguments, VirtualMachine vm)
        {
            base.Execute(arguments, vm);

            int value1 = int.Parse(vm.Variables[arguments[0]]),
                value2 = int.Parse(vm.Variables[arguments[1]]);
            
            vm.ReturnValue = (value1 < value2).ToString();
        }
    }
}
