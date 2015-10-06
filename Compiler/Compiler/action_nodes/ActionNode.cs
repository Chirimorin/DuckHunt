using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.action_nodes
{
    public abstract class ActionNode
    {
        public abstract void Accept(NodeVisitor visitor);
    }
}
