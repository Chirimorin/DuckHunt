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
            setSize(1 + parameters.Length);
            addParameter(0, action);

            int i = 1;
            foreach (string parameter in parameters)
            {
                addParameter(i++, parameter);
            }

            
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
