using System;
using System.Collections.Generic;
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
                Lexer lex = new Lexer(Console.ReadLine());
                Console.WriteLine(lex.Run());
            }
        }
    }
}
