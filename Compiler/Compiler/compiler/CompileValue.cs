using Compiler.action_nodes;
using Compiler.tokenizer;

namespace Compiler.compiler
{
    public class CompileValue : CompiledStatement
    {
        public override CompiledStatement Clone(ref Token currentToken)
        {
            CompiledStatement result = new CompileValue();
            result.Compile(ref currentToken);
            return result;
        }

        public override void Compile(ref Token currentToken)
        {
            if (currentToken.TokenType == Tokens.Number)
                Nodes.Add(new DirectFunctionCall("ConstantToReturn", currentToken.Value));
            else
                Nodes.Add(new DirectFunctionCall("VariableToReturn", currentToken.Value));
            currentToken = currentToken.Next;
        }

        public override bool IsMatch(Token currentToken)
        {
            return currentToken.TokenType == Tokens.Number ||
                currentToken.TokenType == Tokens.Identifier;
        }
    }
}
