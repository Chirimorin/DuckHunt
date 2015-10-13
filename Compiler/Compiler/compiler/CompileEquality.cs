using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.compiler
{
    public class CompileEquality : CompileOperator
    {
        public CompileEquality() : base(new CompileSingleStatement())
        {
            Dictionary<Tokens, string> tokenDictionary = TokenDictionary;
            tokenDictionary.Add(Tokens.Equals, "equals");
            tokenDictionary.Add(Tokens.NotEquals, "notEquals");
            tokenDictionary.Add(Tokens.GreaterEquals, "greaterEquals");
            tokenDictionary.Add(Tokens.SmallerEquals, "smallerEquals");
            tokenDictionary.Add(Tokens.GreaterThan, "greaterThan");
            tokenDictionary.Add(Tokens.SmallerThan, "smallerThan");
        }
    }
}
