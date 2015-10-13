using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.action_nodes
{
    public abstract class AbstractFunctionCall : ActionNode
    {
        private string[] parameters;
        
        public void setSize(int size)
        {
            if (size > 0)
            {
                parameters = new string[size];
            }
            else
            {
                parameters = null;
            }
        }

        public void addParameter(int index, string parameter)
        {
            parameters[index] = parameter;
        }

        public string getParameter(int index)
        {
            return parameters[index];
        }

        public override void Accept(NodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
