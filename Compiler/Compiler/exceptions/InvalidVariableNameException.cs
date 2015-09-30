using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.exceptions
{
    public class InvalidVariableNameException : Exception
    {
        public Token Token { get; set; }

        public InvalidVariableNameException(Token token) 
            : base(String.Format("Invalid variable name \"{0}\" at line {1}, character {2}.", token.Value, token.Line, token.Character))
        {
            Token = token;
        }
    }
}
