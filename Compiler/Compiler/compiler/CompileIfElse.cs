﻿using Compiler.action_nodes;
using Compiler.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.compiler
{
    public class CompileIfElse : CompileIfGeneral
    {
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

        public override LinkedList<ActionNode> compile(Token currentToken)
        {
            int ifLevel = currentToken.Level;

            List<TokenExpectation> expected = new List<TokenExpectation>()
            {
                new TokenExpectation(ifLevel, Tokens.If),
                new TokenExpectation(ifLevel, Tokens.EllipsisOpen),
                new TokenExpectation(ifLevel + 1, Tokens.ANY), // Condition
                new TokenExpectation(ifLevel, Tokens.EllipsisClose),
                new TokenExpectation(ifLevel, Tokens.BracketsOpen),
                new TokenExpectation(ifLevel + 1, Tokens.ANY), // Body
                new TokenExpectation(ifLevel, Tokens.BracketsClose),
                new TokenExpectation(ifLevel, Tokens.Else),
                new TokenExpectation(ifLevel, Tokens.BracketsOpen),
                new TokenExpectation(ifLevel + 1, Tokens.ANY), // Body
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
