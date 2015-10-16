using Compiler.action_nodes;
using Compiler.exceptions;
using Compiler.factories;
using System.Collections.Generic;
using System;

namespace Compiler.compiler
{
    public class CompileIf : CompiledStatement
    {
        public CompileIf()
        {
        }

        public override CompiledStatement Clone(ref Token currentToken)
        {
            CompiledStatement result = new CompileIf();
            result.compile(ref currentToken);
            return result;
        }

        public override void compile(ref Token currentToken)
        {
            // basis opzet if statement
            DoNothing start = new DoNothing();
            DoNothing statementStart = new DoNothing();
            DoNothing end = new DoNothing();
            ConditionalJump conditionalJump = new ConditionalJump(statementStart, end);

            Nodes.add(start);
            Nodes.add(conditionalJump);
            Nodes.add(statementStart);
            Nodes.add(end);
            

            CompileCondition condition = null;
            ActionNode insertPoint = statementStart;

            int ifLevel = currentToken.Level;
            
            List<TokenExpectation> expected = new List<TokenExpectation>()
            {
                new TokenExpectation(ifLevel, Tokens.If),
                new TokenExpectation(ifLevel, Tokens.EllipsisOpen),
                new TokenExpectation(ifLevel + 1, Tokens.ANY),
                new TokenExpectation(ifLevel, Tokens.EllipsisClose),
                new TokenExpectation(ifLevel, Tokens.BracketsOpen),
                new TokenExpectation(ifLevel + 1, Tokens.ANY),
                new TokenExpectation(ifLevel, Tokens.BracketsClose)
            };
            
            foreach (TokenExpectation expectation in expected)
            {
                if (expectation.Level == ifLevel)
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
                else if (expectation.Level > ifLevel)
                {
                    if (condition == null)
                    {
                        condition = new CompileCondition();
                        condition.compile(ref currentToken);
                        Nodes.insertAfter(condition.Nodes, start);
                    }
                    else
                    {
                        while (currentToken.Level > ifLevel)
                        {
                            CompiledStatement statement = CompilerFactory.Instance.CompileStatement(ref currentToken);
                            ActionNode newInsertPoint = statement.Nodes.LastNode;
                            Nodes.insertAfter(statement.Nodes, insertPoint);
                            insertPoint = newInsertPoint;
                        };
                    }
                }
            }
        }

        public override bool IsMatch(Token currentToken)
        {
            return currentToken.TokenType == Tokens.If &&
                currentToken.Partner == null;
        }
    }
}
