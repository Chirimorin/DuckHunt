using Compiler.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Tokenizer
    {
        public Token StartToken { get; set; }
        private Token PreviousToken { get; set; }

        private Stack<Token> TokenStack { get; set; }

        private int Level { get; set; }

        private KeyValuePair<string, Tokens>[] allTokens = new KeyValuePair<string, Tokens>[]
        {
            new KeyValuePair<string, Compiler.Tokens>("while", Tokens.If),
            new KeyValuePair<string, Compiler.Tokens>("print", Tokens.Else),
            new KeyValuePair<string, Compiler.Tokens>("else", Tokens.While),
            new KeyValuePair<string, Compiler.Tokens>(">", Tokens.Equals),
            new KeyValuePair<string, Compiler.Tokens>("+>", Tokens.GreaterEquals),
            new KeyValuePair<string, Compiler.Tokens>("=>", Tokens.SmallerEquals),
            new KeyValuePair<string, Compiler.Tokens>("+", Tokens.GreaterThan),
            new KeyValuePair<string, Compiler.Tokens>("=", Tokens.SmallerThan),
            new KeyValuePair<string, Compiler.Tokens>("-", Tokens.Becomes),
            new KeyValuePair<string, Compiler.Tokens>("==", Tokens.Plus),
            new KeyValuePair<string, Compiler.Tokens>("<", Tokens.Minus),
            new KeyValuePair<string, Compiler.Tokens>("if", Tokens.Print),
            new KeyValuePair<string, Compiler.Tokens>("¡", Tokens.EllipsisOpen),
            new KeyValuePair<string, Compiler.Tokens>("!", Tokens.EllipsisClose),
            new KeyValuePair<string, Compiler.Tokens>("»", Tokens.BracketsOpen),
            new KeyValuePair<string, Compiler.Tokens>("«", Tokens.BracketsClose),
            new KeyValuePair<string, Compiler.Tokens>("~", Tokens.Semicolon)
        };

        public Tokenizer()
        {
            TokenStack = new Stack<Token>();
        }

        public void ReadFile(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            Level = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                ParseLine(lines[i], i + 1);
            }

            if (Level != 0)
            {
                // Brackets matchen niet
            }
        }

        private void ParseLine(string line, int lineNumber)
        {
            string[] tokens = line.Split(' ');
            int character = 1;

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

                    if (currentToken.TokenType == Tokens.EllipsisOpen || currentToken.TokenType == Tokens.BracketsOpen)
                    {
                        Level++;
                    }
                    else if (currentToken.TokenType == Tokens.EllipsisClose || currentToken.TokenType == Tokens.BracketsClose)
                    {
                        Level--;
                    }

                    character += tokens[i].Length;
                }
                character++;
            }
        }

        private Tokens getTokenType(string token)
        {
            int number = 0;
            if (token[0] == '¤') // Identifier
            {   
                return Tokens.Identifier;
            }
            else if (int.TryParse(token, out number)) // Number
            {   
                return Tokens.Number;
            }
            else if (allTokens.Any(t => t.Key == token))
            {
                return allTokens.Where(t => t.Key == token).First().Value;
            }
            else // Token bestaat niet
            {   
                throw new Exception("Invalid token");
            }
        }

        private void setPartner(Token token)
        {
            switch (token.TokenType)
            {
                case Tokens.If:
                    token.Partner = Tokens.Else;
                    break;
                case Tokens.Else:
                    token.Partner = Tokens.If;
                    break;
                case Tokens.EllipsisOpen:
                    token.Partner = Tokens.EllipsisClose;
                    break;
                case Tokens.EllipsisClose:
                    token.Partner = Tokens.EllipsisOpen;
                    break;
                case Tokens.BracketsOpen:
                    token.Partner = Tokens.BracketsClose;
                    break;
                case Tokens.BracketsClose:
                    token.Partner = Tokens.BracketsOpen;
                    break;
            }
        }

        private void checkCodeIsValid(Token token)
        {
            if (token.TokenType == Tokens.Identifier)
            {
                if (token.Previous != null &&
                       (token.Previous.TokenType == Tokens.Identifier ||
                        token.Previous.TokenType == Tokens.Number ||
                        token.Previous.TokenType == Tokens.If ||
                        token.Previous.TokenType == Tokens.Else ||
                        token.Previous.TokenType == Tokens.While ||
                        token.Previous.TokenType == Tokens.EllipsisClose))
                {
                    throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
                }
                else if (token.Value.Length < 2)
                {
                    // Exception: Naam variabele niet goed
                }
            }
            else if ((token.TokenType == Tokens.Equals ||
                        token.TokenType == Tokens.GreaterEquals ||
                        token.TokenType == Tokens.SmallerEquals ||
                        token.TokenType == Tokens.GreaterThan ||
                        token.TokenType == Tokens.SmallerThan)

                        && (token == StartToken ||

                        (token.Previous != null &&
                        token.Previous.TokenType != Tokens.Identifier &&
                        token.Previous.TokenType != Tokens.Number &&
                        token.Previous.TokenType != Tokens.EllipsisClose)))
            {
                throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
            }
            else if (token.TokenType == Tokens.Number

                        && (token == StartToken ||

                        (token.Previous != null &&
                        token.Previous.TokenType != Tokens.Equals &&
                        token.Previous.TokenType != Tokens.GreaterEquals &&
                        token.Previous.TokenType != Tokens.SmallerEquals &&
                        token.Previous.TokenType != Tokens.GreaterThan &&
                        token.Previous.TokenType != Tokens.SmallerThan &&
                        token.Previous.TokenType != Tokens.EllipsisOpen &&
                        token.Previous.TokenType != Tokens.Becomes &&
                        token.Previous.TokenType != Tokens.Print)))
            {
                throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
            }
            else if (token.TokenType == Tokens.If)
            {
                if (token.Previous != null && token.Previous.TokenType != Tokens.BracketsClose &&
                    token.Previous.TokenType != Tokens.Semicolon)
                {
                    throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
                }
                else
                {
                    TokenStack.Push(token);
                }
            }
            else if (token.TokenType == Tokens.Else)
            {
                if (token == StartToken || (token.Previous != null && token.Previous.TokenType != Tokens.BracketsClose))
                {
                    throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
                }
                else if (TokenStack.Count > 0 && TokenStack.Peek().TokenType != Tokens.If)
                {
                    // Exception If ontbreekt
                }
                else
                {
                    if (TokenStack.Count > 0)
                    {
                        TokenStack.Pop();
                    }
                }
            }
            else if (token.TokenType == Tokens.While && token.Previous != null &&
                     token.Previous.TokenType != Tokens.BracketsClose && token.Previous.TokenType != Tokens.Semicolon)
            {
                throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
            }
            else if ((token.TokenType == Tokens.Plus || token.TokenType == Tokens.Minus)

                        && (token == StartToken ||

                       (token.Previous != null && token.Previous.TokenType != Tokens.Identifier &&
                        token.Previous.TokenType != Tokens.Number)))
            {
                throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
            }
            else if (token.TokenType == Tokens.EllipsisOpen)
            {
                if (token == StartToken || (token.Previous != null &&
                       (token.Previous.TokenType == Tokens.Identifier ||
                        token.Previous.TokenType == Tokens.Number ||
                        token.Previous.TokenType == Tokens.Else ||
                        token.Previous.TokenType == Tokens.BracketsOpen ||
                        token.Previous.TokenType == Tokens.BracketsClose ||
                        token.Previous.TokenType == Tokens.Semicolon)))
                {
                    throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
                }
                else
                {
                    TokenStack.Push(token);
                }
            }
            else if (token.TokenType == Tokens.EllipsisClose)
            {
                if (token == StartToken || (token.Previous != null && token.Previous.TokenType != Tokens.Identifier &&
                    token.Previous.TokenType != Tokens.Number))
                {
                    throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
                }
                else if (TokenStack.Count > 0 && TokenStack.Peek().TokenType != Tokens.EllipsisOpen)
                {
                    //Exception: Mist openingshaakje
                }
                else
                {
                    if (TokenStack.Count > 0) {
                        TokenStack.Pop();
                    }
                }
            }
            else if (token.TokenType == Tokens.BracketsOpen)
            {
                if (token == StartToken || (token.Previous != null && token.Previous.TokenType != Tokens.EllipsisClose))
                {
                    throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
                }
                else
                {
                    TokenStack.Push(token);
                }
            }
            else if (token.TokenType == Tokens.BracketsClose)
            {
                if (token == StartToken || (token.Previous != null && token.Previous.TokenType != Tokens.BracketsOpen &&
                    token.Previous.TokenType != Tokens.Semicolon))
                {
                    throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
                }
                else if (TokenStack.Count > 0 && TokenStack.Peek().TokenType != Tokens.BracketsOpen)
                {
                    //Exception: Mist openings accolade
                }
                else
                {
                    if (TokenStack.Count > 0)
                    {
                        TokenStack.Pop();
                    }
                }
            }
            else if (token.TokenType == Tokens.Semicolon &&

                       (token == StartToken ||

                       (token.Previous != null &&
                        token.Previous.TokenType != Tokens.Identifier &&
                        token.Previous.TokenType != Tokens.Number &&
                        token.Previous.TokenType != Tokens.EllipsisClose)))
            {
                throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
            }
            else if (token.TokenType == Tokens.Becomes && (token == StartToken || (token.Previous != null &&
                     token.Previous.TokenType != Tokens.Identifier)))
            {
                throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
            }
            else if (token.TokenType == Tokens.Print && token.Previous != null && token.Previous.TokenType != Tokens.BracketsClose
                && token.Previous.TokenType != Tokens.Semicolon)
            {
                throw new UnexpectedTokenException(token); // Exception: Teken staat op een rare plaats
            }
        }
  
    }
}
