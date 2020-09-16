using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Interpreter
{
    class Lexer
    {
        private string _code;
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






































    }
}
        /*
         
        private string _code;
        private int _position;
        private char _charAt;
        private int _printPosition;
        private int _firstChar;
        private Queue<Token> tq;

        char[] intCollection = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        char[] operators = new char[] { '+', '-', '*', '/' };
        char[] letters = new char[] {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z' };

        public Lexer(string code)
        {
            _code = code;
            tq = Tokenize(_code);
        }


        public Queue<Token> Tokenize(string code)
        {
            Queue<Token> tokens = new Queue<Token>();

            
            for(int i = 0; i < code.Length; i++)
            {
                //char current = Parser();//was a temp variable
                char current = code[i];
                Token _token;

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
            } // end of while loop

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


        public int FirstChar()
        {
            _firstChar = 0;

            while(_code[_firstChar] == ' ')
            {
                _firstChar++;
            }

            return _firstChar;

        }

        /*
        public double GetNum()
        {
            string value = "";
            while (_code[_printPosition] == ' ')
            {
                _printPosition++;
            }
            while(_code[_printPosition] != ' ')
            {
                value += _code[_printPosition];
                _printPosition++;
            }
            return Double.Parse(value);
        }


        public string GetWord(int startChar)
        {
            
            
            string value = "";

            while(_code[startChar] == ' ')
            {
                startChar++;
            }

            while (_code[startChar] != ' ')
            {
                value += _code[startChar];
                startChar++;
                _printPosition++;
            }
            
            _printPosition = startChar;
            return value;
        }

        public string GetKeyword(string value)
        {
                
                switch (value.Trim())
                {
                    case "print":
                        string stringValue = "";
                        while(_code[_printPosition] == ' ')
                        {
                            _printPosition++;
                        }
                        while(_printPosition < _code.Length)
                        {
                            stringValue += _code[_printPosition].ToString();
                            _printPosition++;
                        }
                        
                        return stringValue;
                    case "num":
                        return GetWord(_printPosition);
                    case "string":
                        return "";
                       
                }
                return "";
            
        }
        /*
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


        public string Decision()
        {
            double number;
            if (Double.TryParse(_code[FirstChar()].ToString(), out number))
            {

                return Evaluate();
            }
            else
            {
                return "didnt work";
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


        public string Evaluate()
        {
            Stack<char> opStack = new Stack<char>();
            Stack<string> numAsString = new Stack<string>();
            string numsToAdd = "";

            for (int i = 0; i < tq.Count; i++)
            {
                Token t = tq.Dequeue();
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
            } //end of for loop
            
            numAsString.Push(numsToAdd);
            return numAsString.Pop();
            for (int i = 0; i < opStack.Count; i++)
            {
                double rightSide = Solve(opStack.Pop(), Double.Parse(numAsString.Pop()), Double.Parse(numAsString.Pop()));
                numAsString.Push(rightSide.ToString());
            }

           return numAsString.Pop();
        } //end of number block
            //else if(IsLetter(_code[_firstChar]))
            //{
               // string firstWord = GetWord(_firstChar);
                //return GetKeyword(firstWord);

        //return PrintStatement();
    }
}
        



/*
        public string Run() //was Token
        {

            
            FirstChar();
            if (IsNum(_code[_firstChar])){
                Queue<Token> tl = classLexer.Tokenize();
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
            else if(IsLetter(_code[_firstChar]))
            {
                string firstWord = GetWord(_firstChar);
                return GetKeyword(firstWord);

                //return PrintStatement();
            }
            else
            {
                throw new Exception("Exception");
            }
        } //end of Run method

    } //end of class
}
*/