using Compiler.action_nodes;
using Compiler.exceptions;
using Compiler.factories;
using System.Collections.Generic;

namespace Compiler.compiler
{
    public class CompileIf : CompileIfGeneral
    {
        private CompileCondition _condition;
        public CompileCondition Condition
        {
            get { return _condition; }
            private set { _condition = value; }
        }

        private DoNothing _firstDoNothing;
        public DoNothing FirstDoNothing
        {
            get
            {
                if (_firstDoNothing == null)
                {
                    _firstDoNothing = new DoNothing();
                }
                return _firstDoNothing;
            }
        }

        private ConditionalJump _conditionalJump;
        public ConditionalJump ConditionalJump
        {
            get
            {
                if (_conditionalJump == null)
                {
                    _conditionalJump = new ConditionalJump();
                }
                return _conditionalJump;
            }
        }

        private DoNothing _lastDoNothing;
        public DoNothing LastDoNothing
        {
            get
            {
                if (_lastDoNothing == null)
                {
                    _lastDoNothing = new DoNothing();
                }
                return _lastDoNothing;
            }
        }

        public CompileIf()
        {
            Nodes.insertLast(FirstDoNothing);
            Nodes.insertLast(ConditionalJump);
            Nodes.insertLast(new DoNothing());
            Nodes.insertLast(LastDoNothing);

            ConditionalJump.OnTrueJumpToNode = Nodes.get(3);
            ConditionalJump.OnFalseJumpToNode = Nodes.get(5);
        }

        public override void compile(ref Token currentToken, Token endToken, ActionNodeLinkedList nodes, ActionNode before)
        {
            int ifLevel = currentToken.Level;

            nodes.insertBefore(before, FirstDoNothing);

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
                    if (Condition == null)
                    {
                        Condition = new CompileCondition();
                        Condition.compile(ref currentToken, endToken, nodes, ConditionalJump);
                    }
                    else
                    {
                        while (currentToken.Level > ifLevel)
                        {
                            BaseCompiler compiledBodyPart = Factories.CompilerFactory.Create(currentToken.TokenType);
                            compiledBodyPart.compile(ref currentToken, endToken, nodes, LastDoNothing);
                        };
                    }
                }
            }
        }
    }
}
