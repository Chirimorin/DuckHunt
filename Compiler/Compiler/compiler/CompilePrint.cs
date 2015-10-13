using Compiler.action_nodes;
using Compiler.exceptions;
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

        public override void compile(ref Token currentToken, Token endToken, ActionNodeLinkedList nodes, ActionNode before)
        {
            int printLevel = currentToken.Level;

            List<TokenExpectation> expected = new List<TokenExpectation>()
            {
                new TokenExpectation(printLevel, Tokens.While),
                new TokenExpectation(printLevel, Tokens.EllipsisOpen),
                new TokenExpectation(printLevel + 1, Tokens.ANY), // Condition
                new TokenExpectation(printLevel, Tokens.EllipsisClose)
            };

            foreach (var expectation in expected)
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
                else if (expectation.Level >= printLevel)
                {
                    /*while (currentToken.Value.Level > whileLevel) // Zolang we in de body zitten mag de factory hiermee aan de slag. Dit is niet onze zaak.
                    {
                        var compiledBodyPart = CompilerFactory.Instance.CreateCompiledStatement(currentToken.Value.Token);
                        compiledBodyPart.Compile(ref currentToken, compiler);
                        _body.Add(compiledBodyPart.Compiled);
                    };*/
                }
            }
        }
    }
}
