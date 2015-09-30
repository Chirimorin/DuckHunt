using Compiler.exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            catch (TokenNotFoundException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch (UnexpectedTokenException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
