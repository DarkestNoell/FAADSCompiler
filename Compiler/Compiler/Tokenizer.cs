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
                switch (state)
                {
                    case Types.Space:
                        //if white space

                        //if symbol

                        //if num

                        //if string

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
    }
}
