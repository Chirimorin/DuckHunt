using Compiler.action_nodes;
using Compiler.tokenizer;

namespace Compiler.compiler
{
    public class CompileCondition : CompiledStatement
    {
        public override CompiledStatement Clone(ref Token currentToken)
        {
            CompiledStatement result = new CompileCondition();
            result.Compile(ref currentToken);
            return result;
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

            switch(operatorToken.TokenType)
            {
                case Tokens.Equals: Nodes.Add(new FunctionCall("Equals", leftName, rightName)); break;
                case Tokens.GreaterEquals: Nodes.Add(new FunctionCall("GreaterEquals", leftName, rightName)); break;
                case Tokens.GreaterThan: Nodes.Add(new FunctionCall("GreaterThan", leftName, rightName)); break;
                case Tokens.NotEquals: Nodes.Add(new FunctionCall("NotEquals", leftName, rightName)); break;
                case Tokens.SmallerEquals: Nodes.Add(new FunctionCall("SmallerEquals", leftName, rightName)); break;
                case Tokens.SmallerThan: Nodes.Add(new FunctionCall("SmallerThan", leftName, rightName)); break;
                default: break;
            }
        }

        public override bool IsMatch(Token currentToken)
        {
            return (currentToken.TokenType == Tokens.Number ||
                    currentToken.TokenType == Tokens.Identifier) &&

                   (currentToken.Next.TokenType == Tokens.Equals ||
                    currentToken.Next.TokenType == Tokens.GreaterEquals ||
                    currentToken.Next.TokenType == Tokens.GreaterThan ||
                    currentToken.Next.TokenType == Tokens.NotEquals ||
                    currentToken.Next.TokenType == Tokens.SmallerEquals ||
                    currentToken.Next.TokenType == Tokens.SmallerThan) &&

                   (currentToken.Next.Next.TokenType == Tokens.Number ||
                    currentToken.Next.Next.TokenType == Tokens.Identifier);
        }
    }
}
