using Compiler.action_nodes;
using Compiler.tokenizer;

namespace Compiler.compiler
{
    public class CompileOperator : CompiledStatement
    {
        public override bool IsMatch(Token currentToken)
        {
            return (currentToken.TokenType == Tokens.Number ||
                    currentToken.TokenType == Tokens.Identifier) &&

                   (currentToken.Next.TokenType == Tokens.Minus ||
                    currentToken.Next.TokenType == Tokens.Plus ||
                    currentToken.Next.TokenType == Tokens.Multiply ||
                    currentToken.Next.TokenType == Tokens.Divide) &&

                   (currentToken.Next.Next.TokenType == Tokens.Number ||
                    currentToken.Next.Next.TokenType == Tokens.Identifier);
        }

        public override void Compile(ref Token currentToken)
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
                leftName = NextUniqueId;
                Nodes.Add(new DirectFunctionCall("ConstantToReturn", leftToken.Value));
                Nodes.Add(new DirectFunctionCall("ReturnToVariable", leftName));
            }
            if (rightToken.TokenType != Tokens.Identifier)
            {
                rightName = NextUniqueId;
                Nodes.Add(new DirectFunctionCall("ConstantToReturn", rightToken.Value));
                Nodes.Add(new DirectFunctionCall("ReturnToVariable", rightName));
            }

            switch (operatorToken.TokenType)
            {
                case Tokens.Minus: Nodes.Add(new FunctionCall("Minus", leftName, rightName)); break;
                case Tokens.Plus: Nodes.Add(new FunctionCall("Plus", leftName, rightName)); break;
                case Tokens.Multiply: Nodes.Add(new FunctionCall("Multiply", leftName, rightName)); break;
                case Tokens.Divide: Nodes.Add(new FunctionCall("Divide", leftName, rightName)); break;
                default: break;
            }
        }

        public override CompiledStatement Clone(ref Token currentToken)
        {
            CompiledStatement result = new CompileOperator();
            result.Compile(ref currentToken);
            return result;
        }
    }
}
