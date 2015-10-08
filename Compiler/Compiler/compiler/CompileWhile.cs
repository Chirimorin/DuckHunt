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

        }

        /*public ActionNode getLastToken()
        {

        }*/

    }
}
