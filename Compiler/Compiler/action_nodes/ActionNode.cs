using Compiler.virtual_machine;
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

        public virtual void Execute(VirtualMachine vm) { }
        public abstract void Accept(INodeVisitor visitor);
    }
}
