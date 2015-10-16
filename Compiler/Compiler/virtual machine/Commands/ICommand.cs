using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.virtual_machine.Commands
{
    public interface ICommand
    {
        void Execute(string[] arguments, VirtualMachine vm);
    }
}
