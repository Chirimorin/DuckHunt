using System;

namespace Compiler.exceptions
{
    public class RuntimeException : Exception
    {
        public RuntimeException(string message) : base(message)
        {

        }
    }
}
