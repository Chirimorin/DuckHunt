using Compiler.virtual_machine;

namespace Compiler.action_nodes
{
    public class DirectFunctionCall : AbstractFunctionCall
    {
        public DirectFunctionCall(string action, string parameter)
        {
            ActionName = action;
            Parameters = new string[] { parameter };
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
