using Compiler.action_nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class VirtualMachine
    {
        public void Run(LinkedList<ActionNode> list)
        {
            ActionNode currentNode = list.First();

            while (currentNode != null)
            {
                // Doe iets met de huidige node: 
                //          Command pattern

                // Bepaal de volgende node: 
                //          Visitor pattern
            }
        }

    }
}
