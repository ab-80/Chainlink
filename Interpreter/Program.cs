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
                //Lexer lex = new Lexer(Console.ReadLine());
                //Queue<Token> a = lex.Tokenizer();
                //Console.WriteLine(lex.Evaluator(a));
                Math m = new Math(Console.ReadLine());
                Console.WriteLine(m.EvalPostfix());


                //Console.WriteLine(lex.Decision());//print?
                

                //Console.WriteLine(lex.Evaluate(lex.Tokenize()));

            }
        }
    }
}
