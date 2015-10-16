using Compiler.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            vm.Variables[arguments[0]] = vm.ReturnValue;
            vm.ReturnValue = "";
        }
    }
}
