using Compiler.tokenizer;

namespace Compiler.compiler
{
    public abstract class CompiledStatement
    {
        private static int _localCounter;
        public static string NextUniqueId => "$" + _localCounter++;

        private ActionNodeLinkedList _nodes;
        public ActionNodeLinkedList Nodes => _nodes ?? (_nodes = new ActionNodeLinkedList());

        public abstract void Compile(ref Token currentToken);

        public abstract CompiledStatement Clone(ref Token currentToken);

        public struct TokenExpectation
        {
            public int Level { get; set; }
            public Tokens TokenType { get; set; }

            public TokenExpectation(int level, Tokens tokenType)
            {
                Level = level;
                TokenType = tokenType;
            }
        }

        public abstract bool IsMatch(Token currentToken);
    }
}
