using Compiler.action_nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.compiler
{
    public abstract class BaseCompiler
    {
        //private LinkedList<Node> Actions { get; set; }

        public virtual void compile(Token currentToken)
        {

        }

        public void getLastToken()
        {

        }
    }
}
