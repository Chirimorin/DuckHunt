using System;
using Compiler.tokenizer;

namespace Compiler.exceptions
{
    public class UnexpectedEndOfStatementException : Exception
    {
        public UnexpectedEndOfStatementException(Tokens tokenType) 
            : base(String.Format("Unexpected end of statement, expected {0}", tokenType))
        {
        }
    }
}
