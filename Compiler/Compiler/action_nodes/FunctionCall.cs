using Compiler.virtual_machine;

namespace Compiler.action_nodes
{
    public class FunctionCall : AbstractFunctionCall
    {
        public FunctionCall(string action, params string[] parameters)
        {
            ActionName = action;
            Parameters = parameters;
        }
        
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
