using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
