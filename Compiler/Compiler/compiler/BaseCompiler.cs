using Compiler.action_nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public abstract void compile();

        /*public ActionNode getLastToken()
        {

        }*/
    }
}
