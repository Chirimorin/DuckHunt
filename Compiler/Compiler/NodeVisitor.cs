using Compiler.action_nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public interface NodeVisitor
    {
        void Visit(ActionNode visited);
        void Visit(DoNothing visited);
        void Visit(Jump visited);
        void Visit(ConditionalJump visited);
        void Visit(AbstractFunctionCall visited);
        void Visit(DirectFunctionCall visited);
        void Visit(FunctionCall visited);
    }
}
