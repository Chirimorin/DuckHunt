using System;

namespace Compiler.exceptions
{
    public class BracketsNotMatchingException : Exception
    {
        public BracketsNotMatchingException() : base(String.Format("Brackets not matching")) { }
    }
}
