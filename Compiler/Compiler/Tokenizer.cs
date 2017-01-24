using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Tokenizer
    { //shalom
        enum Types
        {
            Id,
            Symbol,
            Num,
            String,
            Space
        };

        public void Tokenize(string instructions)
        {
            Types state = Types.Space;
            ;
            for (int i = 0; i < instructions.Length; i++)
            {
                char c = instructions[i];
                switch (state)
                {
                    case Types.Space:
                        if (IsWhiteSpace(c))
                        {
                            //do nothing
                        }
                        if (IsSymbol(c))
                        {
                            state = Types.Symbol;
                        }
                        if (IsLetter(c))
                        {
                            state = Types.Id;
                        }
                        if (IsQuotation(c))
                        {
                            state = Types.String;
                        }

                        break;
                    case Types.Symbol:

                        break;
                    case Types.Num:

                        break;
                    case Types.String:

                        break;
                    case Types.Id:

                        break;
                }
            }
            //loop thru each char
                //state machine
        }

        public bool IsWhiteSpace(char c)
        {
            bool isWhiteSpace = false;
            return isWhiteSpace;
        }

        public bool IsSymbol(char c)
        {
            bool isSymbol = false;

            return isSymbol;
        }

        public bool IsNum(char c)
        {
            bool isNum = false;

            return isNum;
        }

        public bool IsQuotation(char c)
        {
            bool isQuotation = false;

            return isQuotation;
        }

        public bool IsLetter(char c)
        {
            bool isLetter = false;

            return isLetter;
        }
    }
}
