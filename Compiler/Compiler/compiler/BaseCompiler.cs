using Compiler.action_nodes;
using Compiler.factories;
using System.Collections.Generic;

namespace Compiler.compiler
{
    public abstract class BaseCompiler
    {
        private static int _localCounter = 0;
        public string LocalCounter
        {
            get {
                _localCounter++;
                return "$" + _localCounter; }
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

        public virtual void compile(ref Token currentToken, Token endToken, ActionNodeLinkedList nodes, ActionNode before)
        {
            while (currentToken != null)
            {
                Factories.CompilerFactory.Create(currentToken.TokenType).compile(ref currentToken, endToken, nodes, before);
            }
        }

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
