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
            Nodes.AddLast(new DoNothing());
            Nodes.AddLast(new DoNothing());
        }

        public abstract override LinkedList<ActionNode> compile(Token currentToken);
    }
}
