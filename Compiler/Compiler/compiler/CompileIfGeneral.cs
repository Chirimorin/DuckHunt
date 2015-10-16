using System;
using Compiler.tokenizer;

namespace Compiler.compiler
{
    public class CompileIfGeneral : CompiledStatement
    {
        public override CompiledStatement Clone(ref Token currentToken)
        {
            throw new NotImplementedException();
        }

        public override void compile(ref Token currentToken)
        {
            throw new NotImplementedException();
        }

        public override bool IsMatch(Token currentToken)
        {
            throw new NotImplementedException();
        }
    }
}
