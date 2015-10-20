﻿namespace Compiler.tokenizer
{
    public class Token
    {
        public Token Previous { get; set; }
        public Token Next { get; set; }

        public Tokens TokenType { get; set; }
        public string Value { get; set; }

        public int Line { get; set; }
        public int Character { get; set; }
        public int Level { get; set; }

        public Token Partner { get; set; }
    }

    public enum Tokens
    {
        Identifier, 
        Equals,
        NotEquals,
        GreaterEquals,
        SmallerEquals,
        GreaterThan,
        SmallerThan,
        Number, 
        If,
        Else,
        While, 
        Plus, 
        Minus,
        EllipsisOpen, 
        EllipsisClose,
        BracketsOpen,
        BracketsClose,
        Semicolon, 
        Becomes, 
        Print,
        Any
    }
}
