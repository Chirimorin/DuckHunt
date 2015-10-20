using Compiler.compiler;
using System;
using System.Collections.Generic;
using Compiler.tokenizer;

namespace Compiler.factories
{
    public class CompilerFactory
    {
        #region Singleton

        private CompilerFactory()
        {
            _compilers = new List<CompiledStatement>
            {
                new CompileWhile(),
                new CompileIf(),
                new CompileIfElse(),
                new CompileAssignment(),
                new CompileOperator(),
                new CompilePrint(),
                new CompileValue()
            };
        }

        public static CompilerFactory Instance { get; } = new CompilerFactory();

        #endregion

        private readonly List<CompiledStatement> _compilers;

        public CompiledStatement CompileStatement(ref Token currentToken)
        {
            foreach (CompiledStatement compiler in _compilers)
            {
                try
                {
                    if (compiler.IsMatch(currentToken))
                        return compiler.Clone(ref currentToken);
                }
                catch (NullReferenceException)
                {
                    // Do nothing
                }
                
            }

            throw new Exception();
        }
    }
}
