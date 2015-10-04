using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.compiler
{
    public class CompileAddtiveOperator : CompileOperator
    {
        public CompileAddtiveOperator()
        {
            Dictionary<Tokens, string> tokenDictionary = TokenDictionary;
            tokenDictionary.Add(Tokens.Plus, "$+");
            tokenDictionary.Add(Tokens.Minus, "$-");
        }
    }
}
