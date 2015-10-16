using Compiler.action_nodes;
using Compiler.factories;
using System;
using Compiler.tokenizer;

namespace Compiler.compiler
{
    public class CompileAssignment : CompiledStatement
    {
        public override CompiledStatement Clone(ref Token currentToken)
        {
            CompiledStatement result = new CompileAssignment();
            result.compile(ref currentToken);
            return result;
        }

        public override void compile(ref Token currentToken)
        {
            string variableName = currentToken.Value;
            currentToken = currentToken.Next.Next;

            CompiledStatement rightCompiled = CompilerFactory.Instance.CompileStatement(ref currentToken);
            Nodes.add(rightCompiled.Nodes);
            Nodes.add(new DirectFunctionCall("ReturnToVariable", variableName));

            if (currentToken.TokenType != Tokens.Semicolon)
                throw new Exception();
            currentToken = currentToken.Next;
        }

        public override bool IsMatch(Token currentToken)
        {
            return currentToken.TokenType == Tokens.Identifier &&
                currentToken.Next.TokenType == Tokens.Becomes;
        }
    }
}
