using Compiler.action_nodes;
using Compiler.exceptions;
using Compiler.factories;
using System.Collections.Generic;
using System;

namespace Compiler.compiler
{
    public class CompileWhile : CompiledStatement
    {
        public CompileWhile()
        {
        }

        public override void compile(ref Token currentToken)
        {
            // basis opzet while loop
            DoNothing start = new DoNothing();
            ConditionalJump conditionalJump = new ConditionalJump();
            DoNothing statementStart = new DoNothing();
            Jump statementEnd = new Jump();
            DoNothing end = new DoNothing();

            Nodes.add(start);
            Nodes.add(conditionalJump);
            Nodes.add(statementStart);
            Nodes.add(statementEnd);
            Nodes.add(end);

            conditionalJump.OnTrueJumpToNode = statementStart;
            conditionalJump.OnFalseJumpToNode = statementEnd;
            statementEnd.JumpToNode = start;


            CompileCondition condition = null;
            ActionNode insertPoint = statementStart;

            int whileLevel = currentToken.Level;
            
            List<TokenExpectation> expected = new List<TokenExpectation>()
            {
                new TokenExpectation(whileLevel, Tokens.While),
                new TokenExpectation(whileLevel, Tokens.EllipsisOpen),
                new TokenExpectation(whileLevel + 1, Tokens.ANY),
                new TokenExpectation(whileLevel, Tokens.EllipsisClose),
                new TokenExpectation(whileLevel, Tokens.BracketsOpen), 
                new TokenExpectation(whileLevel + 1, Tokens.ANY),
                new TokenExpectation(whileLevel, Tokens.BracketsClose)
            };
            
            foreach (TokenExpectation expectation in expected)
            {
                if (expectation.Level == whileLevel)
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
                else if (expectation.Level > whileLevel)
                {
                    if (condition == null)
                    {
                        condition = new CompileCondition();
                        condition.compile(ref currentToken);
                        Nodes.insertAfter(condition.Nodes, start);
                    }
                    else
                    {
                        while (currentToken.Level > whileLevel)
                        {
                            CompiledStatement statement = CompilerFactory.Instance.CompileStatement(ref currentToken);
                            ActionNode newInsertPoint = statement.Nodes.LastNode;
                            Nodes.insertAfter(statement.Nodes, insertPoint);
                            insertPoint = newInsertPoint;
                        }
                    }
                }
            }
        }

        public override bool IsMatch(Token currentToken)
        {
            return currentToken.TokenType == Tokens.While;
        }

        public override CompiledStatement Clone(ref Token currentToken)
        {
            CompiledStatement result = new CompileWhile();
            result.compile(ref currentToken);
            return result;
        }
    }
}
