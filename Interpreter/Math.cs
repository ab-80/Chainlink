using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    class Math
    {
        private string _input;
        public Math(string input)
        {
            _input = input;
        }

        public string QueueToString(Queue<string> q)
        {
            string output = "";

            for(int i = 0; i < q.Count; i++)
            {
                output += q.Dequeue();
            }
            return output;
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

        public bool IsEmpty(Stack<string> stack)
        {
            if(stack.Count == 0)
            {
                return true;
            }
            return false;
        }

        public Queue<string> StringToQueue()
        {
            Queue<string> output = new Queue<string>();
            for(int i = 0; i < _input.Length; i++)
            {
                output.Enqueue(_input[i].ToString());
            }
            return output;
        }

        public double EvalPostfix()
        {  //rename left right in arguments

            Queue<string> temp = new Queue<string>();

            temp = ToPostfix();

            //temp = copyQueue(right);

            Stack<string> newDown = new Stack<string>();

            double rightNumber;
            double leftNumber;
            double result;
            int size = temp.Count;
            for (int i = 0; i < size; i++)
            {
                string toCheck = temp.Dequeue();

                switch (toCheck)
                {
                    case "+":
                        rightNumber = Double.Parse(newDown.Pop());
                        leftNumber = Double.Parse(newDown.Pop());
                        result = leftNumber + rightNumber;
                        newDown.Push(result.ToString());
                        break;

                    case "-":
                        rightNumber = Double.Parse(newDown.Pop());
                        leftNumber = Double.Parse(newDown.Pop());
                        result = leftNumber - rightNumber;
                        newDown.Push(result.ToString());
                        break;

                    case "*":
                        rightNumber = Double.Parse(newDown.Pop());
                        leftNumber = Double.Parse(newDown.Pop());
                        result = leftNumber * rightNumber;
                        newDown.Push(result.ToString());
                        break;

                    case "/":
                        rightNumber = Double.Parse(newDown.Pop());
                        leftNumber = Double.Parse(newDown.Pop());
                        result = leftNumber / rightNumber;
                        newDown.Push(result.ToString());
                        break;

                    default:
                        newDown.Push(toCheck);
                        break;

                }

            }
            double answer = Double.Parse(newDown.Pop());
            return answer;
        }


        public Queue<string> ToPostfix()
        {
            Queue<string> right = new Queue<string>();
            Queue<string> left = new Queue<string>();
            Stack<string> down = new Stack<string>();

            string intToHold = "";
            for (int i = 0; i < _input.Length; i++)
            {
                if(IsNum(_input[i]) || IsOperator(_input[i]))
                {
                    if (IsNum(_input[i]))
                    {
                        intToHold += _input[i];
                    }
                    else
                    {
                        right.Enqueue(intToHold);
                        right.Enqueue(_input[i].ToString());
                        intToHold = "";
                    }
                    
                }
            }
            right.Enqueue(intToHold);
            
            int rightSize = right.Count;
            for (int i = 0; i < rightSize; i++)
            {
                string check1 = right.Dequeue();

                switch (check1)
                {
                    case "+":
                        if (!IsEmpty(down))
                        {
                            while (!IsEmpty(down) && (down.Peek() == ("+") || down.Peek() == ("-") || down.Peek() == ("/") || down.Peek() == ("*")))
                            {
                                string y = down.Pop();
                                left.Enqueue(y);
                            }
                        }
                        down.Push(check1);
                        break;

                    case "-":
                        if (!IsEmpty(down))
                        {
                            while (!IsEmpty(down) && (down.Peek() == ("+") || down.Peek() == ("-") || down.Peek() == ("/") || down.Peek() == ("*")))
                            {
                                string y = down.Pop();
                                left.Enqueue(y);
                            }
                        }
                        down.Push(check1);
                        break;

                    case "*":
                        if (!IsEmpty(down))
                        {
                            while (!IsEmpty(down) && (down.Peek() == ("/") || down.Peek() == ("*")))
                            {
                                string y = down.Pop();
                                left.Enqueue(y);
                            }
                        }
                        down.Push(check1);
                        break;

                    case "/":
                        if (!IsEmpty(down))
                        {
                            while (!IsEmpty(down) && (down.Peek() == ("/") || down.Peek() == ("*")))
                            {
                                string y = down.Pop();
                                left.Enqueue(y);
                            }
                        }
                        down.Push(check1);
                        break;

                    case "(":
                        down.Push(check1);
                        break;

                    case ")":
                        bool complete = false;

                        while (!IsEmpty(down) && !complete)
                        {
                            string token = down.Pop();
                            if (token != ("("))
                            {
                                left.Enqueue(token);
                            }
                            else
                            {
                                complete = true;
                            }
                        }
                        break;

                    default:
                        left.Enqueue(check1);
                        break;
                }
            }
            while (!IsEmpty(down))
            {
                left.Enqueue(down.Pop());
            }
            return left;
        }
    }
}
