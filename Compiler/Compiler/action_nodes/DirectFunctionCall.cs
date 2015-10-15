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
            setSize(2);
            addParameter(0, action);
            addParameter(1, parameter);
        }

        public void action(VirtualMachine virtualMachine)
        {

        }

        public override void Accept(NodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
