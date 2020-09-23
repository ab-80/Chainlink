using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Interpreter
{
    class Lexer
    {
        private readonly string _code;
        private int _printPosition;
        public Lexer(string code)
        {
            _code = code;
            _printPosition = 0;
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
            while (_code[_printPosition] == ' ')
            {
                _printPosition++;
            }
            while(_printPosition < _code.Length && _code[_printPosition] != ' ')
            {
                if(_code[_printPosition] == '(')
                {
                    return value;
                }
                value += _code[_printPosition];
                _printPosition++;
            }
            return value;
        }

        public string Run()
        {
            if(_code.Length == 0)
            {
                return "";
            }
            char firstChar = GetFirstChar();
            if (IsNum(firstChar))
            {
                Math mathClass = new Math(_code);
                return mathClass.EvalPostfix().ToString();
            }
            else
            {
                string firstWord =  GetFirstWord();
                if (firstWord == "print")
                {
                    Print printClass = new Print(_code);
                    return printClass.ConsolePrint(_printPosition);
                }
                return "L";
            }
        }
    }
}
