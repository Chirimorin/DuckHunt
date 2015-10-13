using Compiler.action_nodes;
using Compiler.exceptions;
using Compiler.factories;
using System.Collections.Generic;

namespace Compiler.compiler
{
    public class CompileWhile : BaseCompiler
    {
        private CompileCondition _condition;
        public CompileCondition Condition
        {
            get  { return _condition; }
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

        private Jump _jump;
        public Jump Jump
        {
            get
            {
                if (_jump == null)
                {
                    _jump = new Jump();
                }
                return _jump;
            }
        }

        public CompileWhile()
        {
            Nodes.insertLast(FirstDoNothing);
            Nodes.insertLast(ConditionalJump);
            Nodes.insertLast(new DoNothing());
            Nodes.insertLast(Jump);
            Nodes.insertLast(new DoNothing());

            ConditionalJump.OnTrueJumpToNode = Nodes.get(2);
            ConditionalJump.OnFalseJumpToNode = Nodes.get(4);

            Jump.JumpToNode = Nodes.get(0);
        }

        public override void compile(ref Token currentToken, Token endToken, ActionNodeLinkedList nodes, ActionNode before)
        {
            int whileLevel = currentToken.Level;

            nodes.insertBefore(before, FirstDoNothing);

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
                    if (Condition == null)
                    {
                        Condition = new CompileCondition();
                        Condition.compile(ref currentToken, endToken, nodes, ConditionalJump);
                    }
                    else
                    {
                        BaseCompiler compiledBodyPart = Factories.CompilerFactory.Create(currentToken.TokenType);
                        compiledBodyPart.compile(ref currentToken, endToken, nodes, Jump);
                    }
                }
            }
        }

    }
}
