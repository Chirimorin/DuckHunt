using Compiler.action_nodes;
using System.Collections.Generic;

namespace Compiler.compiler
{
    public abstract class BaseCompiler
    {
        private LinkedList<ActionNode> _nodes;
        public LinkedList<ActionNode> Nodes
        {
            get
            { 
                if (_nodes == null)
                {
                    _nodes = new LinkedList<ActionNode>();
                }
                return _nodes; 
            }
        }

        public abstract LinkedList<ActionNode> compile(Token currentToken, BaseCompiler compiler);

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
    }
}
