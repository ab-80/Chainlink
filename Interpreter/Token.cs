using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    class Token
    {
        private char _value;


        public Token(char value)
        {
            _value = value;
        }

        public char GetTokenValue()
        {
            return _value;
        }
    }
}
