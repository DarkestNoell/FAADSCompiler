using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Tokenizer
    {
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
                        if (isWhiteSpace(c))
                        {
                            //do nothing
                        }
                        if (isSymbol(c))
                        {
                            state = Types.Symbol;
                        }
                        if (isLetter(c))
                        {
                            state = Types.Id;
                        }
                        if (isQuotation(c))
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

        public bool isWhiteSpace(char c)
        {
            
        }

        public bool isSymbol(char c)
        {

        }

        public bool isNum(char c)
        {

        }

        public bool isQuotation(char c)
        {

        }

        public bool isLetter(char c)
        {

        }
    }
}
