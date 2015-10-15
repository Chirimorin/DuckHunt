using Compiler.action_nodes;
using Compiler.compiler;
using Compiler.exceptions;
using Compiler.factories;
using System;

namespace Compiler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("-------- Tokenizer --------");

            Tokenizer tokenizer = new Tokenizer();
            try
            {
                tokenizer.ReadFile("scripts/testScript.txt");
            }
            catch (Exception ex) when
                (ex is TokenNotFoundException ||
                 ex is UnexpectedTokenException ||
                 ex is MissingPartnerTokenException ||
                 ex is BracketsNotMatchingException ||
                 ex is InvalidVariableNameException)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Token token = tokenizer.StartToken;
            while (token != null)
            {
                for (int i = 0; i < token.Level; i++)
                    Console.Write("    ");

                Console.Write(token.TokenType);
                if (token.Partner != null)
                {
                    Console.Write(" - " + token.Partner.TokenType);
                }
                Console.WriteLine();
                token = token.Next;
            }


            Console.WriteLine();
            Console.WriteLine("-------- Compiler --------");
            TokenCompiler compiler = new TokenCompiler();
            ActionNodeLinkedList nodes = compiler.CompileTokens(tokenizer.StartToken);

            ActionNode node = nodes.StartNode;
            while (node != null)
            {
                if (node is DirectFunctionCall)
                {
                    Console.WriteLine("DirectFunctionCall: " + ((DirectFunctionCall)node).getParameter(0));
                }
                else if (node is FunctionCall)
                {
                    Console.WriteLine("FunctionCall: " + ((FunctionCall)node).getParameter(0));
                }
                else
                    Console.WriteLine(node);
                node = node.Next;
            }

            Console.ReadKey();
        }
    }
}
