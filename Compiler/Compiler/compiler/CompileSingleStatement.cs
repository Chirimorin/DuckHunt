using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.action_nodes;

namespace Compiler.compiler
{
    public class CompileSingleStatement : BaseCompiler
    {


        public override void compile(ref Token currentToken, Token endToken, ActionNodeLinkedList nodes, ActionNode before)
        {
            int level = currentToken.Level;
            

        }
    }
}
