using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.virtual_machine.Commands
{
    public class PrintCommand : ICommand
    {
        public void Execute(string[] arguments, VirtualMachine vm)
        {
            Console.WriteLine(vm.ReturnValue);
            vm.ReturnValue = "";
        }
    }
}
