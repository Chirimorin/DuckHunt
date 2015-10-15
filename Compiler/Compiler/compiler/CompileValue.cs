using Compiler.action_nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.compiler
{
    public class CompileValue : CompiledStatement
    {
        public override CompiledStatement Clone(ref Token currentToken)
        {
            CompiledStatement result = new CompileValue();
            result.compile(ref currentToken);
            return result;
        }

        public override void compile(ref Token currentToken)
        {
            if (currentToken.TokenType == Tokens.Number)
                Nodes.add(new DirectFunctionCall("ConstantToReturn", currentToken.Value));
            else
                Nodes.add(new DirectFunctionCall("VariableToReturn", currentToken.Value));
            currentToken = currentToken.Next;
        }

        public override bool IsMatch(Token currentToken)
        {
            return currentToken.TokenType == Tokens.Number ||
                currentToken.TokenType == Tokens.Identifier;
        }
    }
}
