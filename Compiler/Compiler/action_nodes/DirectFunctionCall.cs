using Compiler.virtual_machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.action_nodes
{
    public class DirectFunctionCall : AbstractFunctionCall
    {
        public DirectFunctionCall(string action, string parameter)
        {
            ActionName = action;
            Parameters = new string[] { parameter };
        }

        public void action(VirtualMachine virtualMachine)
        {

        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
