using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.compiler
{
    public class CompileEqualityOperator : CompileOperator
    {
        public CompileEqualityOperator()
        {
            Dictionary<Tokens, string> tokenDictionary = TokenDictionary;
            tokenDictionary.Add(Tokens.Equals, "$==");
        }
    }
}
