using Compiler.compiler;
using Compiler.exceptions;
using System;

namespace Compiler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Tokenizer tokenizer = new Tokenizer();
            try
            {
                tokenizer.ReadFile("scripts/testScript.txt");
            }
            catch (Exception ex)
            {
                if (ex is TokenNotFoundException || ex is UnexpectedTokenException || ex is MissingPartnerTokenException || 
                    ex is BracketsNotMatchingException || ex is InvalidVariableNameException)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            
            Console.WriteLine("Done!");

            Token token = tokenizer.StartToken;
            while (token != null )
            {
                Console.Write(token.TokenType);
                if (token.Partner != null)
                {
                    Console.Write(" - " + token.Partner.TokenType);
                }
                Console.WriteLine();
                token = token.Next;
            }

           // Factories.CompilerFactory.Register(Tokens.While, () => new CompileWhile());

            Console.ReadKey();
        }
    }
}
