using System;
using Compiler.tokenizer;

namespace Compiler.exceptions
{
    public class UnexpectedTokenException : Exception
    {
        public Token Token { get; set; }

        public UnexpectedTokenException(Token token) 
            : base(String.Format("Unexpected token \"{0}\" at line {1}, character {2}.", token.Value, token.Line, token.Character))
        {
            Token = token;
        }
    }
}
