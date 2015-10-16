using System;
using Compiler.tokenizer;

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
