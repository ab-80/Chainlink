using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    class Print
    {
        private string code;

        public Print(string input)
        {
            code = input;
        }
        
        public string ConsolePrint(int printPosition)
        {
            string value = "";
            bool leftParenthese = false;
            bool rightParenthese = false;
            while(leftParenthese == false || rightParenthese == false)
            {
                if (code[printPosition] == '(')
                {
                    leftParenthese = true;
                    printPosition++;
                }
                if (code[printPosition] == ')')
                {
                    rightParenthese = true;
                    printPosition++;
                }
                if (leftParenthese == false || rightParenthese == false)
                {
                    value += code[printPosition];
                    printPosition++;
                }
            }
            return value;
        }
        
    }
}
