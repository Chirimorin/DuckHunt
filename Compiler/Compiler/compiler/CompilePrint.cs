using Compiler.action_nodes;
using Compiler.exceptions;
using Compiler.factories;
using System;
using System.Collections.Generic;
using Compiler.tokenizer;

namespace Compiler.compiler
{
    public class CompilePrint : CompiledStatement
    {
        public override CompiledStatement Clone(ref Token currentToken)
        {
            CompiledStatement result = new CompilePrint();
            result.Compile(ref currentToken);
            return result;
        }

        public override void Compile(ref Token currentToken)
        {
            int printLevel = currentToken.Level;

            List<CompiledStatement.TokenExpectation> expected = new List<CompiledStatement.TokenExpectation>()
            {
                new CompiledStatement.TokenExpectation(printLevel, Tokens.Print),
                new CompiledStatement.TokenExpectation(printLevel, Tokens.EllipsisOpen),
                new CompiledStatement.TokenExpectation(printLevel + 1, Tokens.Any),
                new CompiledStatement.TokenExpectation(printLevel, Tokens.EllipsisClose)
            };

            foreach (CompiledStatement.TokenExpectation expectation in expected)
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
                    Nodes.Add(compiledBodyPart.Nodes);
                }
            }

            Nodes.Add(new DirectFunctionCall("Print", "ReturnValue"));

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
