using Compiler.action_nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.compiler
{
    public abstract class CompileFunction : BaseCompiler
    {
        public CompileFunction()
        {
            //Nodes.AddLast(new DoNothing());
            //Nodes.AddLast(new DoNothing());
        }

        public abstract override void compile(ref Token currentToken, Token endToken, ActionNodeLinkedList nodes, ActionNode before);
    }
}
