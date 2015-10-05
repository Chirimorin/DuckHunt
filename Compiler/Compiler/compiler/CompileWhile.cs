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
        private LinkedList<object> compileStatement;
        private LinkedList<object> condition;
        private LinkedList<object> body;

        public CompileWhile()
        {
            compileStatement = new LinkedList<object>();
            condition = new LinkedList<object>();
            body = new LinkedList<object>();

            var conditionalJump = new ConditionalJump();
            var jump = new Jump();

            compileStatement.AddLast(new DoNothing());
            compileStatement.AddLast(condition);
            compileStatement.AddLast(conditionalJump);
            compileStatement.AddLast(body);
            compileStatement.AddLast(jump);
            compileStatement.AddLast(new DoNothing());


            /*jumpBackNode.JumpToNode = _compiledStatement.First; // JumpToNode is een extra property ten opzichte van andere nodes.
            conditionalJumpNode.NextOnTrue = _body.First; // NextOnTrue en NextOnFalse zijn extra properties ten opzichte van andere nodes.
            conditionalJumpNode.NextOnFalse = _compiledStatement.Last;*/
        }

        public override void compile(Token currentToken)
        {

        }

    }
}
