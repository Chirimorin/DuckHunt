using Compiler.action_nodes;
using Compiler.exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.compiler
{
    public class CompileWhile : BaseCompiler
    {
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

        public override LinkedList<ActionNode> compile(Token currentToken)
        {
            int whileLevel = currentToken.Level;

            List<TokenExpectation> expected = new List<TokenExpectation>()
            {
                new TokenExpectation(whileLevel, Tokens.While),
                new TokenExpectation(whileLevel, Tokens.EllipsisOpen),
                new TokenExpectation(whileLevel + 1, Tokens.ANY), // Condition
                new TokenExpectation(whileLevel, Tokens.EllipsisClose),
                new TokenExpectation(whileLevel, Tokens.BracketsOpen), 
                new TokenExpectation(whileLevel + 1, Tokens.ANY), // Body
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
                    /*if (_condition == null) // We komen eerst de conditie tegen, deze vullen we daarom eerst.
                    {
                        var compiledCondition = new CompiledCondition();
                        compiledCondition.Compile(ref currentToken, compiler);
                        _condition.Add(compiledCondition.Compiled);
                    }
                    else
                    {
                        while (currentToken.Value.Level > whileLevel) // Zolang we in de body zitten mag de factory hiermee aan de slag. Dit is niet onze zaak.
                        {
                            var compiledBodyPart = CompilerFactory.Instance.CreateCompiledStatement(currentToken.Value.Token);
                            compiledBodyPart.Compile(ref currentToken, compiler);
                            _body.Add(compiledBodyPart.Compiled);
                        };
                    }*/
                }
            }

            return Nodes;
        }

    }
}
