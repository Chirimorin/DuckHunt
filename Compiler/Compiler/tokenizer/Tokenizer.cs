using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.exceptions;

namespace Compiler.tokenizer
{
    public class Tokenizer
    {
        public Token StartToken { get; set; }
        private Token PreviousToken { get; set; }

        private Stack<Token> TokenStack { get; }

        private int Level { get; set; }

        #region all tokens
        private readonly Dictionary<string, Tokens> _allTokens = new Dictionary<string, Tokens>
        {
            {"while", Tokens.If},
            {"print", Tokens.Else},
            {"else", Tokens.While},
            {">", Tokens.Equals},
            {"?>", Tokens.NotEquals},
            {"+>", Tokens.GreaterEquals},
            {"=>", Tokens.SmallerEquals},
            {"+", Tokens.GreaterThan},
            {"=", Tokens.SmallerThan},
            {"-", Tokens.Becomes},
            {"==", Tokens.Plus},
            {"<", Tokens.Minus},
            {"if", Tokens.Print},
            {"¡", Tokens.EllipsisOpen},
            {"!", Tokens.EllipsisClose},
            {"»", Tokens.BracketsOpen},
            {"«", Tokens.BracketsClose},
            {"~", Tokens.Semicolon}
        };
        #endregion

        #region push to stack tokens

        readonly Tokens[] _pushToStackTokens = 
        {
            Tokens.If,
            Tokens.EllipsisOpen,
            Tokens.BracketsOpen
        };
        #endregion

        #region partners
        private readonly Dictionary<Tokens, Tokens> _partners = new Dictionary<Tokens, Tokens>
        {
            {Tokens.Else, Tokens.If },
            {Tokens.EllipsisClose, Tokens.EllipsisOpen },
            {Tokens.BracketsClose, Tokens.BracketsOpen }
        };
        #endregion

        #region ignore tokens
        private readonly Tokens[] _ignoreTokens = 
        {
            Tokens.Else,
            Tokens.BracketsClose,
            Tokens.BracketsOpen,
            Tokens.EllipsisOpen,
            Tokens.EllipsisClose
        };
        #endregion

        #region increase/decrease tokens
        private readonly Tokens[] _increaseTokens = 
        {
            Tokens.EllipsisOpen,
            Tokens.BracketsOpen
        };

        private readonly Tokens[] _decreaseTokens = 
        {
            Tokens.EllipsisClose,
            Tokens.BracketsClose
        };
        #endregion

        #region possible start tokens

        private readonly Tokens[] _possibleStartTokens = 
        {
            Tokens.Identifier,
            Tokens.If,
            Tokens.While,
            Tokens.Print
        };
        #endregion

        #region possible previous tokens
        private readonly Dictionary<Tokens, Tokens[]> _possiblePreviousTokens = new Dictionary<Tokens, Tokens[]>
        {
            {Tokens.If, new Tokens[] {
                Tokens.BracketsOpen,
                Tokens.BracketsClose,
                Tokens.Semicolon } },
            {Tokens.Else, new Tokens[] {
                Tokens.BracketsClose } },
            {Tokens.While, new Tokens[] {
                Tokens.BracketsOpen,
                Tokens.BracketsClose,
                Tokens.Semicolon } },
            {Tokens.Equals, new Tokens[] {
                Tokens.Identifier,
                Tokens.Number,
                Tokens.EllipsisClose } },
            {Tokens.NotEquals, new Tokens[] {
                Tokens.Identifier,
                Tokens.Number,
                Tokens.EllipsisClose } },
            {Tokens.GreaterEquals, new Tokens[] {
                Tokens.Identifier,
                Tokens.Number,
                Tokens.EllipsisClose } },
            {Tokens.SmallerEquals, new Tokens[] {
                Tokens.Identifier,
                Tokens.Number,
                Tokens.EllipsisClose } },
            {Tokens.GreaterThan, new Tokens[] {
                Tokens.Identifier,
                Tokens.Number,
                Tokens.EllipsisClose } },
            {Tokens.SmallerThan, new Tokens[] {
                Tokens.Identifier,
                Tokens.Number,
                Tokens.EllipsisClose } },
            {Tokens.Becomes, new Tokens[] {
                Tokens.Identifier } },
            {Tokens.Plus, new Tokens[] {
                Tokens.Identifier,
                Tokens.Number,
                Tokens.EllipsisClose } },
            {Tokens.Minus, new Tokens[] {
                Tokens.Identifier,
                Tokens.Number,
                Tokens.EllipsisClose } },
            {Tokens.Print, new Tokens[] {
                Tokens.BracketsOpen,
                Tokens.BracketsClose,
                Tokens.Semicolon } },
            {Tokens.EllipsisOpen, new Tokens[] {
                Tokens.Equals,
                Tokens.GreaterEquals,
                Tokens.SmallerEquals,
                Tokens.GreaterThan,
                Tokens.SmallerThan,
                Tokens.If,
                Tokens.While,
                Tokens.Plus,
                Tokens.Minus,
                Tokens.EllipsisOpen,
                Tokens.Becomes,
                Tokens.Print } },
            {Tokens.EllipsisClose, new Tokens[] {
                Tokens.Identifier,
                Tokens.Number,
                Tokens.EllipsisClose } },
            {Tokens.BracketsOpen, new Tokens[] {
                Tokens.Else,
                Tokens.EllipsisClose } },
            {Tokens.BracketsClose, new Tokens[] {
                Tokens.BracketsClose,
                Tokens.Semicolon } },
            {Tokens.Semicolon, new Tokens[] {
                Tokens.Identifier,
                Tokens.Number,
                Tokens.EllipsisClose } }
        };
        #endregion

        public Tokenizer()
        {
            TokenStack = new Stack<Token>();
        }

        public void ReadFile(string path)
        {
            Level = 0;

            string[] lines = System.IO.File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                ParseLine(lines[i], i + 1);
            }

            if (Level != 0)
            {
                throw new BracketsNotMatchingException(); // Exception: Brackets matchen niet
            }
        }

        private void ParseLine(string line, int lineNumber)
        {
            int character = 1;

            string[] tokens = line.Split(' ');
            foreach (string token in tokens)
            {
                if (token.Length != 0)
                {
                    Token currentToken = new Token
                    {
                        Value = token,
                        Line = lineNumber,
                        Character = character,
                        Level = Level
                    };

                    if (StartToken == null)
                        StartToken = currentToken;

                    if (PreviousToken != null)
                    {
                        currentToken.Previous = PreviousToken;
                        PreviousToken.Next = currentToken;
                    }

                    try
                    {
                        currentToken.TokenType = GetTokenType(token);
                    }
                    catch (Exception)
                    {
                        throw new TokenNotFoundException(currentToken);
                    }
                    
                    SetPartner(currentToken);

                    CheckCodeIsValid(currentToken);

                    // Haalt "if" van de stack af als er geen "else" is
                    if (TokenStack.Count != 0)
                    {
                        Token topToken = TokenStack.Peek();

                        if (topToken.TokenType == Tokens.If &&
                            currentToken.Level <= topToken.Level &&
                            !_ignoreTokens.Contains(currentToken.TokenType))
                        {
                            TokenStack.Pop();
                        }
                    }

                    // Pusht token naar de stack of haalt partner token eraf als dat nodig is
                    Tokens partner;
                    if (_pushToStackTokens.Contains(currentToken.TokenType))
                    {
                        TokenStack.Push(currentToken);
                    }
                    else if (TokenStack.Count != 0 &&
                             _partners.TryGetValue(currentToken.TokenType, out partner) &&
                             TokenStack.Peek().TokenType == partner)
                    {
                        TokenStack.Pop();
                    }

                    if (_increaseTokens.Contains(currentToken.TokenType))
                        Level++;
                    else if (_decreaseTokens.Contains(currentToken.TokenType))
                    {
                        Level--;
                        currentToken.Level--;
                    }

                    character += token.Length;
                    PreviousToken = currentToken;
                }
                character++;
            }
        }

        private Tokens GetTokenType(string token)
        {
            int number = 0;
            Tokens result;

            if (token[0] == '¤') // Identifier
                return Tokens.Identifier;

            if (int.TryParse(token, out number)) // Number
                return Tokens.Number;

            if (_allTokens.TryGetValue(token, out result)) // Ander token
                return result;
          
            // Token bestaat niet
            throw new Exception("Invalid token");
        }

        private void SetPartner(Token token)
        {
            Tokens partner;
            if (!_partners.TryGetValue(token.TokenType, out partner) || TokenStack.Count == 0 ||
                TokenStack.Peek().TokenType != partner) return;
            token.Partner = TokenStack.Peek();
            token.Partner.Partner = token;
        }

        private void CheckCodeIsValid(Token token)
        {
            Tokens[] result;
            if (_possiblePreviousTokens.TryGetValue(token.TokenType, out result))
            {
                if ((token == StartToken && !_possibleStartTokens.Contains(token.TokenType)) ||
                    (token.Previous != null && !result.Contains(token.Previous.TokenType)))
                {
                    throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
                }
            }

            Tokens partner;
            if (_partners.TryGetValue(token.TokenType, out partner))
            {
                if (TokenStack.Count == 0 || TokenStack.Peek().TokenType != partner)
                {
                    throw new MissingPartnerTokenException(token); // Exception: Partner token ontbreekt
                }
            }

            if (token.TokenType == Tokens.Identifier && token.Value.Length < 2)
            {
                throw new InvalidVariableNameException(token); // Exception: Naam variabele niet goed
            }
        }
  
    }
}
