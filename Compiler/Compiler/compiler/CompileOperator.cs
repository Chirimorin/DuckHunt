using Compiler.action_nodes;
using Compiler.tokenizer;

namespace Compiler.compiler
{
    public class CompileOperator : CompiledStatement
    {
        public CompileOperator()
        {
        }

        public override bool IsMatch(Token currentToken)
        {
            return (currentToken.TokenType == Tokens.Number ||
                    currentToken.TokenType == Tokens.Identifier) &&

                   (currentToken.Next.TokenType == Tokens.Minus ||
                    currentToken.Next.TokenType == Tokens.Plus) &&

                   (currentToken.Next.Next.TokenType == Tokens.Number ||
                    currentToken.Next.Next.TokenType == Tokens.Identifier);
        }

        public override void compile(ref Token currentToken)
        {
            Token leftToken = currentToken;
            string leftName = leftToken.Value;
            currentToken = currentToken.Next;
            Token operatorToken = currentToken;
            currentToken = currentToken.Next;
            Token rightToken = currentToken;
            string rightName = rightToken.Value;
            currentToken = currentToken.Next;

            if (leftToken.TokenType != Tokens.Identifier)
            {
                leftName = NextUniqueID;
                Nodes.add(new DirectFunctionCall("ConstantToReturn", leftToken.Value));
                Nodes.add(new DirectFunctionCall("ReturnToVariable", leftName));
            }
            if (rightToken.TokenType != Tokens.Identifier)
            {
                rightName = NextUniqueID;
                Nodes.add(new DirectFunctionCall("ConstantToReturn", rightToken.Value));
                Nodes.add(new DirectFunctionCall("ReturnToVariable", rightName));
            }

            switch (operatorToken.TokenType)
            {
                case Tokens.Minus: Nodes.add(new FunctionCall("Minus", leftName, rightName)); break;
                case Tokens.Plus: Nodes.add(new FunctionCall("Plus", leftName, rightName)); break;
                default: break;
            }
        }

        public override CompiledStatement Clone(ref Token currentToken)
        {
            CompiledStatement result = new CompileOperator();
            result.compile(ref currentToken);
            return result;
        }
    }
}
