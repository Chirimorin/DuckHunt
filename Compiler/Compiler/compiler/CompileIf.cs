using Compiler.action_nodes;
using Compiler.exceptions;
using Compiler.factories;
using System.Collections.Generic;
using Compiler.tokenizer;

namespace Compiler.compiler
{
    public class CompileIf : CompiledStatement
    {
        public override CompiledStatement Clone(ref Token currentToken)
        {
            CompiledStatement result = new CompileIf();
            result.Compile(ref currentToken);
            return result;
        }

        public override void Compile(ref Token currentToken)
        {
            // basis opzet if statement
            DoNothing start = new DoNothing();
            DoNothing statementStart = new DoNothing();
            DoNothing end = new DoNothing();
            ConditionalJump conditionalJump = new ConditionalJump(statementStart, end);

            Nodes.Add(start);
            Nodes.Add(conditionalJump);
            Nodes.Add(statementStart);
            Nodes.Add(end);
            

            CompileCondition condition = null;
            ActionNode insertPoint = statementStart;

            int ifLevel = currentToken.Level;
            
            List<TokenExpectation> expected = new List<TokenExpectation>()
            {
                new TokenExpectation(ifLevel, Tokens.If),
                new TokenExpectation(ifLevel, Tokens.EllipsisOpen),
                new TokenExpectation(ifLevel + 1, Tokens.Any),
                new TokenExpectation(ifLevel, Tokens.EllipsisClose),
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
                    if (condition == null)
                    {
                        condition = new CompileCondition();
                        condition.Compile(ref currentToken);
                        Nodes.InsertAfter(condition.Nodes, start);
                    }
                    else
                    {
                        while (currentToken.Level > ifLevel)
                        {
                            CompiledStatement statement = CompilerFactory.Instance.CompileStatement(ref currentToken);
                            ActionNode newInsertPoint = statement.Nodes.LastNode;
                            Nodes.InsertAfter(statement.Nodes, insertPoint);
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
