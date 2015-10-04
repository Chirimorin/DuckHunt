using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.compiler
{
    public class CompileRelationalOperator : CompileOperator
    {
        public CompileRelationalOperator()
        {
            Dictionary<Tokens, string> tokenDictionary = TokenDictionary;
            tokenDictionary.Add(Tokens.GreaterEquals, "$>=");
            tokenDictionary.Add(Tokens.SmallerEquals, "$<=");
            tokenDictionary.Add(Tokens.GreaterThan, "$>");
            tokenDictionary.Add(Tokens.SmallerThan, "$<");
        }
    }
}
