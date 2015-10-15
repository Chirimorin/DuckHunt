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
    public class CompileIfElse : CompiledStatement
    {
        public CompileIfElse()
        {
        }

        public override CompiledStatement Clone(ref Token currentToken)
        {
            CompiledStatement result = new CompileIfElse();
            result.compile(ref currentToken);
            return result;
        }

        public override void compile(ref Token currentToken)
        {
            // basis opzet if statement
            DoNothing start = new DoNothing();
            ConditionalJump conditionalJump = new ConditionalJump();
            DoNothing ifStart = new DoNothing();
            Jump ifEnd = new Jump();
            DoNothing elseStart = new DoNothing();
            DoNothing elseEnd = new DoNothing();

            Nodes.add(start);
            Nodes.add(conditionalJump);
            Nodes.add(ifStart);
            Nodes.add(ifEnd);
            Nodes.add(elseStart);
            Nodes.add(elseEnd);

            conditionalJump.OnTrueJumpToNode = ifStart;
            conditionalJump.OnFalseJumpToNode = elseStart;
            ifEnd.JumpToNode = elseEnd;

            bool conditionCompiled = false;
            bool ifCompiled = false;

            ActionNode insertPointIf = ifStart;
            ActionNode insertPointElse = elseStart;

            int ifLevel = currentToken.Level;

            List<TokenExpectation> expected = new List<TokenExpectation>()
            {
                new TokenExpectation(ifLevel, Tokens.If),
                new TokenExpectation(ifLevel, Tokens.EllipsisOpen),
                new TokenExpectation(ifLevel + 1, Tokens.ANY),
                new TokenExpectation(ifLevel, Tokens.EllipsisClose),
                new TokenExpectation(ifLevel, Tokens.BracketsOpen),
                new TokenExpectation(ifLevel + 1, Tokens.ANY),
                new TokenExpectation(ifLevel, Tokens.BracketsClose),
                new TokenExpectation(ifLevel, Tokens.Else),
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
                    if (!conditionCompiled)
                    {
                        CompileCondition condition = new CompileCondition();
                        condition.compile(ref currentToken);
                        Nodes.insertAfter(condition.Nodes, start);
                        conditionCompiled = true;
                    }
                    else if (!ifCompiled)
                    {
                        while (currentToken.Level > ifLevel)
                        {
                            CompiledStatement statement = CompilerFactory.Instance.CompileStatement(ref currentToken);
                            ActionNode newInsertPoint = statement.Nodes.LastNode;
                            Nodes.insertAfter(statement.Nodes, insertPointIf);
                            insertPointIf = newInsertPoint;
                        }
                        ifCompiled = true;
                    }
                    else
                    {
                        while (currentToken.Level > ifLevel)
                        {
                            CompiledStatement statement = CompilerFactory.Instance.CompileStatement(ref currentToken);
                            ActionNode newInsertPoint = statement.Nodes.LastNode;
                            Nodes.insertAfter(statement.Nodes, insertPointElse);
                            insertPointElse = newInsertPoint;
                        }
                    }
                }
            }
        }

        public override bool IsMatch(Token currentToken)
        {
            return currentToken.TokenType == Tokens.If &&
                currentToken.Partner.TokenType == Tokens.Else;
        }
    }
}
