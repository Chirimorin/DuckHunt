using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class TokenNotFoundException : Exception
    {
        public Token Token { get; set; }

        public TokenNotFoundException(Token token) 
            : base(String.Format("Invalid token \"{0}\" at line {1}, character {2}.", token.Value, token.Line, token.Character))
        {
            Token = token;
        }
    }
}
