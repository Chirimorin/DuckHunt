using Compiler.action_nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override void compile(LinkedList<Token> currentToken)
        {
            //int whileLevel = currentToken.Value.Level;
            /*int whileLevel = 0;

            List<TokenExpectation> expected = new List<TokenExpectation>()
            {
                new TokenExpectation(whileLevel, Tokens.While), 
                new TokenExpectation(whileLevel, Tokens.EllipsisOpen),
                //new TokenExpectation(whileLevel + 1, TokenType.ANY), // Condition
                new TokenExpectation(whileLevel, Tokens.EllipsisClose),
                new TokenExpectation(whileLevel, Tokens.BracketsOpen), 
                //new TokenExpectation(whileLevel + 1, TokenType.ANY), // Body
                new TokenExpectation(whileLevel, Tokens.BracketsClose)
            };*/
        }

        /*public ActionNode getLastToken()
        {

        }*/


        /*public struct TokenExpectation
        {
            public int Level { get; set; }
            public Tokens TokenType { get; set; }

            public TokenExpectation(int level, Tokens tokenType)
            {
                Level = level;
                TokenType = tokenType;
            }
        }*/

    }
}
