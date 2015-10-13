using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.action_nodes;

namespace Compiler.compiler
{
    public class CompileCondition : CompileOperator
    {
        public CompileCondition () : base(new CompileSingleStatement())
        {
            Dictionary<Tokens, string> tokenDictionary = TokenDictionary;

            
            
        }
        

    }
}
