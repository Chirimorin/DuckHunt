using Compiler.action_nodes;

namespace Compiler.virtual_machine
{
    public interface INodeVisitor
    {
        void Visit(ConditionalJump visited);
        void Visit(DirectFunctionCall visited);
        void Visit(DoNothing visited);
        void Visit(FunctionCall visited);
        void Visit(Jump visited);
    }
}
