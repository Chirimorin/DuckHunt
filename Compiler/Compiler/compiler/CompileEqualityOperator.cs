using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.compiler
{
    public class CompileEqualityOperator : CompileOperator
    {
        public CompileEqualityOperator() : base(new CompileAddtiveOperator())
        {
            Dictionary<Tokens, string> tokenDictionary = TokenDictionary;
            tokenDictionary.Add(Tokens.Equals, "$==");
            tokenDictionary.Add(Tokens.NotEquals, "$!=");
        }
    }
}
