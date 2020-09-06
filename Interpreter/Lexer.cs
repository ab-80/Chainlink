using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Interpreter
{
    class Lexer
    {
        private string _code;
        private int _position;
        private Token _token;
        private char _charAt;
        private int _printPosition;

        char[] intCollection = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        char[] operators = new char[] { '+', '-', '*', '/' };
        char[] letters = new char[] {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z' };

        public Lexer(string code)
        {
            _code = code;
            _charAt = 'a';
            _position = -1;
        }


        public char Parser()
        {
            ++_position;
            _charAt = _code[_position];

            return _charAt;
        }


        public Queue<Token> Tokenize()
        {
            Queue<Token> tokens = new Queue<Token>();
            string keyWord = "";

            while (_position < _code.Length - 1)
            {
                char current = Parser();//was a temp variable

                switch (current)
                {
                    case '+':
                        _token = new Token("plus", current);
                        tokens.Enqueue(_token);
                        break;
                    case '-':
                        _token = new Token("minus", current);
                        tokens.Enqueue(_token);
                        break;
                    case '*':
                        _token = new Token("mult", current);
                        tokens.Enqueue(_token);
                        break;
                    case '/':
                        _token = new Token("div", current);
                        tokens.Enqueue(_token);
                        break;
                    case ' ':
                        break;
                }

                if (IsNum(current))
                {
                    _token = new Token("num", current);
                    tokens.Enqueue(_token);
                }

                if (IsLetter(current)){
                    keyWord += current;
                    if(keyWord == "print")
                    {

                    }
                }
            }

            return tokens;
        }




        public string Tostring(List<char> list1)
        {
            string u = "";
            for (int i = 0; i < list1.Count; i++)
            {
                char c = list1[i];
                string s = c.ToString();
                u += s;

            }
            return u;
        }


        public bool IsLetter(char input)
        {
            for(int i = 0; i < letters.Length; i++)
            {
                if(input == letters[i])
                {
                    return true;
                }
            }
            return false;
        }



        public bool IsNum(char input)
        {
            bool value = false;
            for (int i = 0; i < intCollection.Length; i++)
            {
                if (input == intCollection[i] || input == '.')
                {
                    value = true;
                }
            }

            return value;

        }


        public bool IsOperator(char input)
        {
            bool value = false;
            for (int i = 0; i < operators.Length; i++)
            {
                if (input == operators[i])
                {
                    value = true;
                }
            }
            return value;
        }


        public string Word()
        {
            string value = "";
            _printPosition = 0;
            try
            {
                if(_code[_printPosition] == ' ')
                {
                    _printPosition++;
                }
                while (_code[_printPosition] != ' ')
                {
                    value += _code[_printPosition].ToString();
                    _printPosition++;
                }
                return value;
            }
            catch(Exception e)
            {
                return value;
            }
        }

        public string PrintStatement()
        {
            string value = "";
            _printPosition++;

            while (_printPosition < _code.Length)
            {
                value += _code[_printPosition];
                _printPosition++;
            }
            return value;
        }


        public int PrecedenceNumber(char input)
        {
            if (input == '+' || input == '-')
            {
                return 0;
            }
            else if (input == '*' || input == '/')
            {
                return 1;
            }
            else
            {
                throw new Exception("input Error with precedence number");
            }
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



        public string Run() //was Token
        {
            if (IsNum(_code[0])){
                Lexer lex = new Lexer(_code);
                Queue<Token> tl = lex.Tokenize();
                Stack<char> opStack = new Stack<char>();
                Stack<string> numAsString = new Stack<string>();
                string numsToAdd = "";
                int count = tl.Count;


                for (int i = 0; i < count; i++)
                {
                    Token t = tl.Dequeue();
                    char tok = t.GetTokenValue();

                    if (IsNum(tok))
                    {
                        numsToAdd += tok.ToString();
                    }
                    else//if the token is not a number
                    {
                        numAsString.Push(numsToAdd);
                        numsToAdd = "";

                        if (IsOperator(tok))
                        {
                            if (opStack.Count == 0) //tok gets pushed right away if it's the first one
                            {
                                opStack.Push(tok);
                            }
                            else //if the operator stack is not empty
                            {
                                char prevOperator = opStack.Peek();

                                if (ComparePrecedence(prevOperator, tok)) //if earlier operator has higher precedence/equal
                                {
                                    char o = opStack.Pop();
                                    double number2 = Double.Parse(numAsString.Pop());
                                    double number1 = Double.Parse(numAsString.Pop());

                                    numAsString.Push(Solve(o, number2, number1).ToString()); //push final answer back onto intAsString
                                    opStack.Push(tok); //finally pushing the token onto the operator stack
                                }
                                else //if current tok being pushed has higher precedence
                                {
                                    opStack.Push(tok);
                                }
                            }
                        }
                        else //if the token is neither a number or operator
                        {
                            throw new Exception("Token was neither an int or operator");
                        }
                    }
                } //end of foreach loop
                numAsString.Push(numsToAdd);
                for (int i = 0; i < opStack.Count; i++)
                {
                    double rightSide = Solve(opStack.Pop(), Double.Parse(numAsString.Pop()), Double.Parse(numAsString.Pop()));
                    numAsString.Push(rightSide.ToString());
                }

                return numAsString.Pop();
            } //end of number block
            else if(Word() == "print")
            {
                return PrintStatement();
            }
            else
            {
                throw new Exception("Exception");
            }
        } //end of Run method

    } //end of class
}
