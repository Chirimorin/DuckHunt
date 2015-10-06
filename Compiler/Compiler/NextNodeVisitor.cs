using Compiler.action_nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class NextNodeVisitor : NodeVisitor
    {
        public void Visit(Jump visited)
        {
            throw new NotImplementedException();
        }

        public void Visit(AbstractFunctionCall visited)
        {
            throw new NotImplementedException();
        }

        public void Visit(FunctionCall visited)
        {
            throw new NotImplementedException();
        }

        public void Visit(DirectFunctionCall visited)
        {
            throw new NotImplementedException();
        }

        public void Visit(ConditionalJump visited)
        {
            throw new NotImplementedException();
        }

        public void Visit(DoNothing visited)
        {
            throw new NotImplementedException();
        }

        public void Visit(ActionNode visited)
        {
            throw new NotImplementedException();
        }
    }
}
