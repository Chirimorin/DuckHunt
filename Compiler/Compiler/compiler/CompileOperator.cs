﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.compiler
{
    public class CompileOperator : Compiler
    {
        private Dictionary<Tokens, string> tokenDictionary = new Dictionary<Tokens, string>();
        public Dictionary<Tokens, string>  TokenDictionary 
        {
            get
            {
                return tokenDictionary;
            }
        }
    }
}
