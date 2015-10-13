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
            parameters = new string[size];
        }

        public void setParameter(string parameter, int index)
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
