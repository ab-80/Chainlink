using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Interpreter
{
    class Lexer
    {
        private readonly string _code;
        public Lexer(string code)
        {
            _code = code;
        }

        public Queue<Token> Tokenizer()
        {
            Queue<Token> tokenQueue = new Queue<Token>();

            for (int i = 0; i < _code.Length; i++)
            {
                char currentChar = _code[i];

                if (currentChar != ' ')
                {
                    Token t = new Token(currentChar);
                    tokenQueue.Enqueue(t);
                }
            }
            return tokenQueue;
        }

        public string Evaluator(Queue<Token> tokenQueue)
        {
            Stack<Token> opStack = new Stack<Token>();
            Stack<double> numStack = new Stack<double>();
            Stack<string> rightSide = new Stack<string>();
            string numAsString = "";

            for (int i = 0; i < tokenQueue.Count; i++)
            {
                
                Token t = tokenQueue.Dequeue();
                if (IsNum(t.GetTokenValue()))
                {
                    
                    numAsString += t.GetTokenValue().ToString();
                    while (tokenQueue.Count != 0 && IsNum(tokenQueue.Peek().GetTokenValue()))
                    {
                        numAsString += tokenQueue.Dequeue().GetTokenValue().ToString();
                    }
                    numStack.Push(Double.Parse(numAsString));
                }
                else if (IsOperator(t.GetTokenValue()))
                {
                    if (opStack.Count == 0)
                    {
                        opStack.Push(t);
                    }
                    else
                    {
                        if(ComparePrecedence(opStack.Peek().GetTokenValue(), t.GetTokenValue()))
                        {
                            numStack.Push(Solve(opStack.Pop().GetTokenValue(), Double.Parse(rightSide.Pop()), Double.Parse(rightSide.Pop())));
                            opStack.Push(t);
                        }
                        else
                        {
                            numStack.Push(Solve(t.GetTokenValue(), Double.Parse(rightSide.Pop()), Double.Parse(rightSide.Pop())));
                        }
                    }
                }
                else
                {
                    throw new Exception("error");
                }
            }
            /*
            if (numAsString != "")
            {
                rightSide.Push(numAsString);
            }
            */
            
            string toAdd = "";
            for (int i = 0; i < tokenQueue.Count; i++)
            {
                tokenQueue.Dequeue();
            }
            numStack.Push(Double.Parse(toAdd));
            return tokenQueue.Count.ToString();

            for (int i = 0; i < opStack.Count; i++)
            {
                numStack.Push(Solve(opStack.Pop().GetTokenValue(), Double.Parse(rightSide.Pop()), Double.Parse(rightSide.Pop())));
            }
            return numStack.Pop().ToString();
        }


        public bool IsNum(char input)
        {
            char[] intCollection = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < intCollection.Length; i++)
            {
                if (input == intCollection[i] || input == '.')
                {
                    return true;
                }
            }
            return false;
        }


        public bool IsOperator(char input)
        {
            char[] operators = new char[] { '+', '-', '*', '/' };
            for (int i = 0; i < operators.Length; i++)
            {
                if (input == operators[i])
                {
                    return true;
                }
            }
            return false;
        }

        public int PrecedenceNumber(char input)
        {
            if (input == '+' || input == '-')
            {
                return 0;
            }
            else if(input == '*' || input == '/')
            {
                return 1;
            }
            throw new Exception("error");
        }

        public Boolean ComparePrecedence(char first, char second)
        {
            int firstInt = PrecedenceNumber(first);
            int secondInt = PrecedenceNumber(second);

            if (firstInt > secondInt || firstInt == secondInt)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double Solve(char o, double number2, double number1)
        {
            switch (o)
            {
                case '+':
                    return (number1 + number2);
                case '-':
                    return (number1 - number2);
                case '*':
                    return (number1 * number2);
                case '/':
                    return (number1 / number2);
            }
            throw new Exception("error");
        }

        public char GetFirstChar()
        {
            for(int i = 0; i < _code.Length; i++)
            {
                if(_code[i] != ' ')
                {
                    return _code[i];
                }
            }
            return '!';
        }

        public string GetFirstWord()
        {
            string value = "";
            int i = 0;
            while (_code[i] == ' ')
            {
                i++;
            }
            while(i < _code.Length && _code[i] != ' ')
            {
                value += _code[i];
                i++;
            }
            return value;
        }

        public string Run()
        {
            char firstChar = GetFirstChar();
            if (IsNum(firstChar))
            {
                Math mathClass = new Math(_code);
                return mathClass.EvalPostfix().ToString();
            }
            else
            {
                return GetFirstWord();
            }
        }
    }
}
