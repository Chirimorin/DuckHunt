using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.action_nodes;

namespace Compiler.compiler
{
    public class CompileIfGeneral : CompiledStatement
    {
        public override CompiledStatement Clone(ref Token currentToken)
        {
            throw new NotImplementedException();
        }

        public override void compile(ref Token currentToken)
        {
            throw new NotImplementedException();
        }

        public override bool IsMatch(Token currentToken)
        {
            throw new NotImplementedException();
        }
    }
}
