using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.compiler
{
    public class CompileComperison : CompileOperator
    {
        public CompileComperison() : base(new CompilePlusMin())
        {
            Dictionary<Tokens, string> tokenDictionary = TokenDictionary;
            tokenDictionary.Add(Tokens.Equals, "$==");
            tokenDictionary.Add(Tokens.NotEquals, "$!=");
            tokenDictionary.Add(Tokens.GreaterEquals, "$>=");
            tokenDictionary.Add(Tokens.SmallerEquals, "$<=");
            tokenDictionary.Add(Tokens.GreaterThan, "$>");
            tokenDictionary.Add(Tokens.SmallerThan, "$<");
        }
    }
}
