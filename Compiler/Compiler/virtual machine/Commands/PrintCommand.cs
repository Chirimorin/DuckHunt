using System;

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
