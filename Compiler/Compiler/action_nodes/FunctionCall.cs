using Compiler.virtual_machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.action_nodes
{
    public class FunctionCall : AbstractFunctionCall
    {
        public FunctionCall(string action, params string[] parameters)
        {
            ActionName = action;
            Parameters = parameters;
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
