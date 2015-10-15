using Compiler.action_nodes;
using Compiler.factories;
using System.Collections.Generic;

namespace Compiler.compiler
{
    public abstract class CompiledStatement
    {
        private static int _localCounter = 0;
        public static string NextUniqueID
        {
            get
            {
                return "$" + _localCounter++;
            }
        }

        private ActionNodeLinkedList _nodes;
        public ActionNodeLinkedList Nodes
        {
            get
            { 
                if (_nodes == null)
                {
                    _nodes = new ActionNodeLinkedList();
                }
                return _nodes; 
            }
        }

        public abstract void compile(ref Token currentToken);

        public abstract CompiledStatement Clone(ref Token currentToken);

        /*public ActionNode getLastToken()
        {

        }*/

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
