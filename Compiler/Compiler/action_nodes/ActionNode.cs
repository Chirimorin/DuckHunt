using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.action_nodes
{
    public abstract class ActionNode
    {
        public ActionNode Next { get; set; }

        public abstract void Accept(NodeVisitor visitor);
    }
}
