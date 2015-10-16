using Compiler.action_nodes;
using Compiler.exceptions;
using Compiler.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.compiler
{
    public class CompilePrint : CompileFunction
    {
        public CompilePrint(): base() { }

        public override CompiledStatement Clone(ref Token currentToken)
        {
            CompiledStatement result = new CompilePrint();
            result.compile(ref currentToken);
            return result;
        }

        public override void compile(ref Token currentToken)
        {
            int printLevel = currentToken.Level;

            List<TokenExpectation> expected = new List<TokenExpectation>()
            {
                new TokenExpectation(printLevel, Tokens.Print),
                new TokenExpectation(printLevel, Tokens.EllipsisOpen),
                new TokenExpectation(printLevel + 1, Tokens.ANY),
                new TokenExpectation(printLevel, Tokens.EllipsisClose)
            };

            foreach (TokenExpectation expectation in expected)
            {
                if (expectation.Level == printLevel)
                {
                    if (currentToken.TokenType != expectation.TokenType)
                    {
                        throw new UnexpectedEndOfStatementException(expectation.TokenType);
                    }
                    else
                    {
                        currentToken = currentToken.Next;
                    }
                }
                else if (expectation.Level > printLevel)
                {
                    var compiledBodyPart = CompilerFactory.Instance.CompileStatement(ref currentToken);
                    Nodes.add(compiledBodyPart.Nodes);
                }
            }

            Nodes.add(new DirectFunctionCall("Print", "ReturnValue"));

            if (currentToken.TokenType != Tokens.Semicolon)
                throw new Exception();

            currentToken = currentToken.Next;
        }

        public override bool IsMatch(Token currentToken)
        {
            return currentToken.TokenType == Tokens.Print;
        }
    }
}
