using Compiler.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Compiler
{
    public class Tokenizer
    {
        public Token StartToken { get; set; }
        private Token PreviousToken { get; set; }

        private Stack<Token> TokenStack { get; set; }

        private int Level { get; set; }

        #region all tokens
        private Dictionary<string, Tokens> allTokens = new Dictionary<string, Tokens>()
        {
            {"while", Tokens.If},
            {"print", Tokens.Else},
            {"else", Tokens.While},
            {">", Tokens.Equals},
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
        Tokens[] pushToStackTokens = new Tokens[]
        {
            Tokens.If,
            Tokens.EllipsisOpen,
            Tokens.BracketsOpen
        };
        #endregion

        #region partners
        private Dictionary<Tokens, Tokens> partners = new Dictionary<Tokens, Tokens>()
        {
            {Tokens.Else, Tokens.If },
            {Tokens.EllipsisClose, Tokens.EllipsisOpen },
            {Tokens.BracketsClose, Tokens.BracketsOpen }
        };
        #endregion

        #region ignore tokens
        Tokens[] ignoreTokens = new Tokens[]
        {
            Tokens.Else,
            Tokens.BracketsClose,
            Tokens.BracketsOpen,
            Tokens.EllipsisOpen,
            Tokens.EllipsisClose
        };
        #endregion

        #region increase/decrease tokens
        Tokens[] increaseTokens = new Tokens[]
        {
            Tokens.EllipsisOpen,
            Tokens.BracketsOpen
        };

        Tokens[] decreaseTokens = new Tokens[]
        {
            Tokens.EllipsisClose,
            Tokens.BracketsClose
        };
        #endregion

        #region possible start tokens
        Tokens[] possibleStartTokens = new Tokens[]
        {
            Tokens.Identifier,
            Tokens.If,
            Tokens.While,
            Tokens.Print
        };
        #endregion

        #region possible previous tokens
        private Dictionary<Tokens, Tokens[]> possiblePreviousTokens = new Dictionary<Tokens, Tokens[]>()
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
            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].Length != 0)
                {
                    Token currentToken = new Token();
                    currentToken.Value = tokens[i];
                    currentToken.Line = lineNumber;
                    currentToken.Character = character;
                    currentToken.Level = Level;

                    if (StartToken == null)
                        StartToken = currentToken;

                    if (PreviousToken != null)
                    {
                        currentToken.Previous = PreviousToken;
                        PreviousToken.Next = currentToken;
                    }

                    try
                    {
                        currentToken.TokenType = getTokenType(tokens[i]);
                    }
                    catch (Exception)
                    {
                        throw new TokenNotFoundException(currentToken);
                    }
                    
                    setPartner(currentToken);

                    checkCodeIsValid(currentToken);

                    // Haalt "if" van de stack af als er geen "else" is
                    if (TokenStack.Count != 0)
                    {
                        Token topToken = TokenStack.Peek();

                        if (topToken.TokenType == Tokens.If &&
                            currentToken.Level <= topToken.Level &&
                            !ignoreTokens.Contains(currentToken.TokenType))
                        {
                            TokenStack.Pop();
                        }
                    }

                    //if ((currentToken.TokenType == Tokens.Identifier ||
                    //     currentToken.TokenType == Tokens.If ||
                    //     currentToken.TokenType == Tokens.While ||
                    //     currentToken.TokenType == Tokens.Print) &&
                    //     TokenStack.Count > 0 && TokenStack.Peek().TokenType == Tokens.If)
                    //{
                    //    TokenStack.Pop();
                    //}

                    // Pusht token naar de stack of haalt partner token eraf als dat nodig is
                    Tokens partner;
                    if (pushToStackTokens.Contains(currentToken.TokenType))
                    {
                        TokenStack.Push(currentToken);
                    }
                    else if (TokenStack.Count != 0 &&
                            partners.TryGetValue(currentToken.TokenType, out partner) &&
                            TokenStack.Peek().TokenType == partner)
                    {
                        TokenStack.Pop();
                    }

                    if (increaseTokens.Contains(currentToken.TokenType))
                        Level++;
                    else if (decreaseTokens.Contains(currentToken.TokenType))
                        Level--;

                    character += tokens[i].Length;
                    PreviousToken = currentToken;
                }
                character++;
            }
        }

        private Tokens getTokenType(string token)
        {
            int number = 0;
            Tokens result;

            if (token[0] == '¤') // Identifier
                return Tokens.Identifier;

            else if (int.TryParse(token, out number)) // Number
                return Tokens.Number;

            else if (allTokens.TryGetValue(token, out result)) // Ander token
                return result;
          
            else // Token bestaat niet
                throw new Exception("Invalid token");
        }

        private void setPartner(Token token)
        {
            Tokens partner;
            if (partners.TryGetValue(token.TokenType, out partner) &&
                TokenStack.Count != 0 &&
                TokenStack.Peek().TokenType == partner)
            {
                token.Partner = TokenStack.Peek();
                token.Partner.Partner = token;
            }
        }

        private void checkCodeIsValid(Token token)
        {
            Tokens[] result;
            if (possiblePreviousTokens.TryGetValue(token.TokenType, out result))
            {
                if ((token == StartToken && !possibleStartTokens.Contains(token.TokenType)) ||
                    (token.Previous != null && !result.Contains(token.Previous.TokenType)))
                {
                    throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
                }
            }

            Tokens partner;
            if (partners.TryGetValue(token.TokenType, out partner))
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
