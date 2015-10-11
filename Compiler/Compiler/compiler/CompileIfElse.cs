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
    public class CompileIfElse : CompileIfGeneral
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

        private LinkedList<ActionNode> _bodyElse;
        public LinkedList<ActionNode> BodyElse
        {
            get
            {
                if (_bodyElse == null)
                {
                    _bodyElse = new LinkedList<ActionNode>();
                }
                return _bodyElse;
            }
        }

        public CompileIfElse()
        {
            ConditionalJump conditionalJump = new ConditionalJump();
            Jump jump = new Jump();

            Nodes.AddLast(new DoNothing());
            Nodes.AddLast(conditionalJump);
            Nodes.AddLast(new DoNothing());

            Nodes.AddLast(jump);
            Nodes.AddLast(new DoNothing());
            Nodes.AddLast(new DoNothing());

            conditionalJump.OnTrueJumpToNode = Nodes.ElementAt(3);
            conditionalJump.OnFalseJumpToNode = Nodes.ElementAt(6);

            jump.JumpToNode = Nodes.ElementAt(8);
        }

        public override LinkedList<ActionNode> compile(Token currentToken, BaseCompiler compiler)
        {
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

            foreach (var expectation in expected)
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
                else if (expectation.Level >= ifLevel)
                {
                    if (Condition.Count <= 0)
                    {
                        CompileCondition compiledCondition = new CompileCondition();
                        compiledCondition.compile(currentToken, compiler);
                        //Condition.AddLast(compiledCondition.Compiled);
                    }
                    else if (Body.Count <= 0)
                    {
                        while (currentToken.Level > ifLevel)
                        {
                            BaseCompiler compiledBodyPart = Factories.CompilerFactory.Create(currentToken.TokenType);
                            compiledBodyPart.compile(currentToken, compiler);
                            //Body.AddLast(compiledBodyPart.Compiled);
                        };
                    }
                    else
                    {
                        while (currentToken.Level > ifLevel)
                        {
                            BaseCompiler compiledBodyPart = Factories.CompilerFactory.Create(currentToken.TokenType);
                            compiledBodyPart.compile(currentToken, compiler);
                            //BodyElse.AddLast(compiledBodyPart.Compiled);
                        };
                    }
                }
            }

            return Nodes;
        }
    }
}
