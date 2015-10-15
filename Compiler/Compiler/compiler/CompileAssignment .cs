using Compiler.action_nodes;
using Compiler.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.compiler
{
    public class CompileAssignment : BaseCompiler
    {
        public override void compile(ref Token currentToken, Token endToken, ActionNodeLinkedList nodes, ActionNode before)
        {
            string variableName = currentToken.Value;
            currentToken = currentToken.Next.Next;


   

        }

    }
}
