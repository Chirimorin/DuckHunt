using Compiler.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.virtual_machine.Commands
{
    public class ConstantToReturnCommand : ICommand
    {
        public void Execute(string[] arguments, VirtualMachine vm)
        {
            if (arguments == null || arguments.Length != 1)
            {
                throw new RuntimeException("Incorrect arguments for ConstantToReturnCommand");
            }

            vm.ReturnValue = arguments[0];
        }
    }
}
