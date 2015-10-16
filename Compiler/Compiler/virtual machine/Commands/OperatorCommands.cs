using Compiler.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.virtual_machine.Commands
{
    public abstract class OperatorCommand : ICommand
    {
        public virtual void Execute(string[] arguments, VirtualMachine vm)
        {
            if (arguments == null || arguments.Length != 2)
            {
                throw new RuntimeException("Incorrect amount of arguments for OperatorCommand");
            }
        }
    }

    public class PlusCommand : OperatorCommand
    {
        public override void Execute(string[] arguments, VirtualMachine vm)
        {
            base.Execute(arguments, vm);

            int value1 = int.Parse(vm.Variables[arguments[0]]),
                value2 = int.Parse(vm.Variables[arguments[1]]);

            vm.ReturnValue = (value1 + value2).ToString();
        }
    }

    public class MinusCommand : OperatorCommand
    {
        public override void Execute(string[] arguments, VirtualMachine vm)
        {
            base.Execute(arguments, vm);

            int value1 = int.Parse(vm.Variables[arguments[0]]),
                value2 = int.Parse(vm.Variables[arguments[1]]);

            vm.ReturnValue = (value1 - value2).ToString();
        }
    }
}
