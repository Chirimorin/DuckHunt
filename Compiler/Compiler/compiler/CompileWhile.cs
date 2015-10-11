using Compiler.action_nodes;
using Compiler.exceptions;
using Compiler.factories;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.compiler
{
    public class CompileWhile : BaseCompiler
    {
        private LinkedList<ActionNode> _condition;
        public LinkedList<ActionNode> Condition
        {
            get
            {
                if (_condition == null)
                {
                    _condition = new LinkedList<ActionNode>();
                }
                return _condition;
            }
        }

        private LinkedList<ActionNode> _body;
        public LinkedList<ActionNode> Body
        {
            get
            {
                if (_body == null)
                {
                    _body = new LinkedList<ActionNode>();
                }
                return _body;
            }
        }

        public CompileWhile()
        {
            ConditionalJump conditionalJump = new ConditionalJump();
            Jump jump = new Jump();

            Nodes.AddLast(new DoNothing());
            Nodes.AddLast(conditionalJump);
            Nodes.AddLast(new DoNothing());
            Nodes.AddLast(jump);
            Nodes.AddLast(new DoNothing());

            conditionalJump.OnTrueJumpToNode = Nodes.ElementAt(2);
            conditionalJump.OnFalseJumpToNode = Nodes.ElementAt(4);

            jump.JumpToNode = Nodes.ElementAt(0);
        }

        public override LinkedList<ActionNode> compile(Token currentToken, BaseCompiler compiler)
        {
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

            foreach (var expectation in expected)
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
                else if (expectation.Level >= whileLevel)
                {
                    if (Condition.Count <= 0)
                    {
                        CompileCondition compiledCondition = new CompileCondition();
                        compiledCondition.compile(currentToken, compiler);
                        //Condition.AddLast(compiledCondition.Compiled);
                    }
                    else
                    {
                        while (currentToken.Level > whileLevel)
                        {
                            BaseCompiler compiledBodyPart = Factories.CompilerFactory.Create(currentToken.TokenType);
                            compiledBodyPart.compile(currentToken, compiler);
                            //Body.AddLast(compiledBodyPart.Compiled);
                        };
                    }
                }
            }

            return Nodes;
        }

    }
}
