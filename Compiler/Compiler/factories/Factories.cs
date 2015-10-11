using Compiler;
using Compiler.action_nodes;
using Compiler.compiler;
using Compiler.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.factories
{
    public static class Factories
    {
        private static readonly GenericFactory<Tokens, BaseCompiler> _compilerFactory = new GenericFactory<Tokens, BaseCompiler>();
        public static GenericFactory<Tokens, BaseCompiler> CompilerFactory
        {
            get { return _compilerFactory; }
        }

        /*public static BaseCompiler createCompiler(Tokens tokenType)
        {
            switch (tokenType)
            {
                case Tokens.Identifier:
                case Tokens.Equals:
                case Tokens.GreaterEquals:
                case Tokens.SmallerEquals:
                case Tokens.GreaterThan:
                case Tokens.SmallerThan:
                case Tokens.Number:
                case Tokens.If:
                case Tokens.Else:
                case Tokens.While:
                    return new CompileWhile();
                case Tokens.Plus:
                case Tokens.Minus:
                case Tokens.EllipsisOpen:
                case Tokens.EllipsisClose:
                case Tokens.BracketsOpen:
                case Tokens.BracketsClose:
                case Tokens.Semicolon:
                case Tokens.Becomes:
                case Tokens.Print:
                default:
                    throw new ArgumentException("Unknown token type: " + tokenType, "tokenType");
            }
        }*/

    }
}
