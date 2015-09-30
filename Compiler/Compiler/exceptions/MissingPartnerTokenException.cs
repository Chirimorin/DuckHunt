using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.exceptions
{
    public class MissingPartnerTokenException : Exception
    {
        public Token Token { get; set; }

        public MissingPartnerTokenException(Token token) 
            : base(String.Format("Missing partner token by \"{0}\" token at line {1}, character {2}.", token.Value, token.Line, token.Character))
        {
            Token = token;
        }
    }
}
