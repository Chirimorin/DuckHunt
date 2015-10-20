using Compiler.action_nodes;
using Compiler.exceptions;
using Compiler.factories;
using System.Collections.Generic;
using Compiler.tokenizer;

namespace Compiler.compiler
{
    public class CompileIfElse : CompiledStatement
    {
        public override CompiledStatement Clone(ref Token currentToken)
        {
            CompiledStatement result = new CompileIfElse();
            result.Compile(ref currentToken);
            return result;
        }

        public override void Compile(ref Token currentToken)
        {
            // basis opzet if statement
            DoNothing start = new DoNothing();
            DoNothing ifStart = new DoNothing();
            Jump ifEnd = new Jump();
            DoNothing elseStart = new DoNothing();
            DoNothing elseEnd = new DoNothing();
            ConditionalJump conditionalJump = new ConditionalJump(ifStart, elseStart);

            Nodes.Add(start);
            Nodes.Add(conditionalJump);
            Nodes.Add(ifStart);
            Nodes.Add(ifEnd);
            Nodes.Add(elseStart);
            Nodes.Add(elseEnd);
            
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
                new TokenExpectation(ifLevel + 1, Tokens.Any),
                new TokenExpectation(ifLevel, Tokens.EllipsisClose),
                new TokenExpectation(ifLevel, Tokens.BracketsOpen),
                new TokenExpectation(ifLevel + 1, Tokens.Any),
                new TokenExpectation(ifLevel, Tokens.BracketsClose),
                new TokenExpectation(ifLevel, Tokens.Else),
                new TokenExpectation(ifLevel, Tokens.BracketsOpen),
                new TokenExpectation(ifLevel + 1, Tokens.Any),
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
                        condition.Compile(ref currentToken);
                        Nodes.InsertAfter(condition.Nodes, start);
                        conditionCompiled = true;
                    }
                    else if (!ifCompiled)
                    {
                        while (currentToken.Level > ifLevel)
                        {
                            CompiledStatement statement = CompilerFactory.Instance.CompileStatement(ref currentToken);
                            ActionNode newInsertPoint = statement.Nodes.LastNode;
                            Nodes.InsertAfter(statement.Nodes, insertPointIf);
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
                            Nodes.InsertAfter(statement.Nodes, insertPointElse);
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
