using Compiler;
using Compiler.action_nodes;
using Compiler.compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public static class CompileFactory
    {
        public static BaseCompiler createCompiler(Tokens tokenType)
        {
            switch (tokenType)
            {
                /*case Tokens.Identifier:
                case Tokens.Equals:
                case Tokens.GreaterEquals:
                case Tokens.SmallerEquals:
                case Tokens.GreaterThan:
                case Tokens.SmallerThan:
                case Tokens.Number:
                case Tokens.If:
                case Tokens.Else:*/
                case Tokens.While:
                    return new CompileWhile();
                /*case Tokens.Plus:
                case Tokens.Minus:
                case Tokens.EllipsisOpen:
                case Tokens.EllipsisClose:
                case Tokens.BracketsOpen:
                case Tokens.BracketsClose:
                case Tokens.Semicolon:
                case Tokens.Becomes:
                case Tokens.Print:*/
                default:
                    throw new ArgumentException("Unknown token type: " + tokenType, "tokenType");
            }
        }

    }
}
