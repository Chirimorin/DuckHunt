using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer tokenizer = new Tokenizer();
            try
            {
                tokenizer.ReadFile("Script2.txt");
            }
            catch (TokenNotFoundException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }


            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
