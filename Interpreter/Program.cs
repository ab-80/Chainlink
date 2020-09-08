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
                Lexer lex = new Lexer();

                Console.WriteLine(lex.Run());

            }
        }
    }
}
