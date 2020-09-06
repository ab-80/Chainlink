using System;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string a = Console.ReadLine();

                Lexer lex = new Lexer(a);
                Console.WriteLine(lex.Run());

            }
        }
    }
}
