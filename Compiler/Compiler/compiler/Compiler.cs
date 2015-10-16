using Compiler.factories;
using Compiler.tokenizer;

namespace Compiler.compiler
{
    public class TokenCompiler
    {
        private ActionNodeLinkedList _compiledNodes;

        public TokenCompiler()
        {
            _compiledNodes = new ActionNodeLinkedList();
        }

        public ActionNodeLinkedList CompileTokens(Token currentToken)
        {
            while (currentToken != null)
            {
                _compiledNodes.add(CompilerFactory.Instance.CompileStatement(ref currentToken).Nodes);
            }

            return _compiledNodes;
        }
    }
}
