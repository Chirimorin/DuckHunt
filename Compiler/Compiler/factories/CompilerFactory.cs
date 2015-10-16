using Compiler.compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.factories
{
    public class CompilerFactory
    {
        #region Singleton
        private static CompilerFactory _instance = new CompilerFactory();

        private CompilerFactory()
        {
            _compilers = new List<CompiledStatement>();
            _compilers.Add(new CompileWhile());
            _compilers.Add(new CompileIf());
            _compilers.Add(new CompileIfElse());
            _compilers.Add(new CompileAssignment());
            _compilers.Add(new CompileOperator());
            _compilers.Add(new CompilePrint());

            // Onderaan laten staan!
            _compilers.Add(new CompileValue());
        }

        public static CompilerFactory Instance
        {
            get { return _instance; }
        }
        #endregion

        private List<CompiledStatement> _compilers;

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
