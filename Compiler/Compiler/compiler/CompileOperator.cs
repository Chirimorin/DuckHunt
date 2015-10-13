using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.compiler
{
    public class CompileOperator : BaseCompiler
    {
        private Dictionary<Tokens, string> _tokenDictionary = new Dictionary<Tokens, string>();
        protected Dictionary<Tokens, string>  TokenDictionary 
        {
            get { return _tokenDictionary; }
        }

        private BaseCompiler _nextCompiler;
        protected BaseCompiler NextCompiler
        {
            get { return _nextCompiler; }
            private set { _nextCompiler = value; }
        }

        public CompileOperator(BaseCompiler compiler)
        {
            _nextCompiler = compiler;
        }
        
    }
}
