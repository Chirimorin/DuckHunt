using Compiler.action_nodes;
using Compiler.tokenizer;

namespace Compiler.compiler
{
    public class CompileCondition : CompiledStatement
    {
        public override CompiledStatement Clone(ref Token currentToken)
        {
            CompiledStatement result = new CompileCondition();
            result.compile(ref currentToken);
            return result;
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

            switch(operatorToken.TokenType)
            {
                case Tokens.Equals: Nodes.add(new FunctionCall("Equals", leftName, rightName)); break;
                case Tokens.GreaterEquals: Nodes.add(new FunctionCall("GreaterEquals", leftName, rightName)); break;
                case Tokens.GreaterThan: Nodes.add(new FunctionCall("GreaterThan", leftName, rightName)); break;
                case Tokens.NotEquals: Nodes.add(new FunctionCall("NotEquals", leftName, rightName)); break;
                case Tokens.SmallerEquals: Nodes.add(new FunctionCall("SmallerEquals", leftName, rightName)); break;
                case Tokens.SmallerThan: Nodes.add(new FunctionCall("SmallerThan", leftName, rightName)); break;
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
