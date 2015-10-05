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
        private LinkedList<Node> compiledStatement;
        private LinkedList<Node> condition;
        private LinkedList<Node> body;

        public CompileWhile()
        {
            compiledStatement = new LinkedList<Node>();
            condition = new LinkedList<Node>();
            body = new LinkedList<Node>();

            var conditionalJump = new ConditionalJump();
            var jump = new Jump();

            compiledStatement.AddLast(new DoNothing());
            compiledStatement.AddLast(condition);
            compiledStatement.AddLast(conditionalJump);
            compiledStatement.AddLast(body);
            compiledStatement.AddLast(jump);
            compiledStatement.AddLast(new DoNothing());


            /*jumpBackNode.JumpToNode = _compiledStatement.First; // JumpToNode is een extra property ten opzichte van andere nodes.
            conditionalJumpNode.NextOnTrue = _body.First; // NextOnTrue en NextOnFalse zijn extra properties ten opzichte van andere nodes.
            conditionalJumpNode.NextOnFalse = _compiledStatement.Last;*/
        }

    }
}
