using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    class Token
    {
        private string _type;
        private char _value;


        public Token(string type, char value)
        {
            _type = type;
            _value = value;
        }

        public char GetTokenValue()
        {
            return _value;
        }
    }
}
