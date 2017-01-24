using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Tokenizer
    { //shalom

        List<Symbol> tokens = new List<Symbol>();
        public void Tokenize(string instructions)
        {
            EState state = EState.Space;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < instructions.Length; i++)
            {
                char c = instructions[i];
                switch (state)
                {
                    case EState.Space:
                        if (IsWhiteSpace(c))
                        {
                            //do nothing
                        }
                        else if (IsSymbol(c))
                        {
                            state = EState.Symbol;
                        }
                        else if (IsLetter(c))
                        {
                            state = EState.Id;
                        }
                        else if (IsQuotation(c))
                        {
                            state = EState.String;
                        }else if (IsNum(c))
                        {
                            
                        }

                        break;
                    case EState.Symbol:
                        if (IsWhiteSpace(c))
                        {
                            Symbol token = new Symbol(sb.ToString(), state);
                            tokens.Add(token);
                            state = EState.Space;
                            sb.Clear();
                        }
                        else if (IsLetter(c))
                        {
                            
                        }
                        else if (IsSymbol(c))
                        {
                        }
                        else if (IsQuotation(c))
                        {
                        }

                        break;
                    case EState.Num:
                        if (IsWhiteSpace(c))
                        {
                        }
                        else if (IsSymbol(c))
                        {
                        }
                        else if (IsLetter(c))
                        {
                        }
                        else if (IsQuotation(c))
                        {
                        }
                        else if (IsNum(c))
                        {

                        }
                        break;
                    case EState.String:
                        if (IsWhiteSpace(c))
                        {
                            //do nothing
                        }
                        else if (IsSymbol(c))
                        {
                            
                        }
                        else if (IsLetter(c))
                        {
                        }
                        else if (IsQuotation(c))
                        {
                        }
                        else if (IsNum(c))
                        {

                        }
                        break;
                    case EState.Id:
                        if (IsWhiteSpace(c))
                        {
                        }
                        else if (IsSymbol(c))
                        {
                        }
                        else if (IsLetter(c))
                        {
                        }
                        else if (IsQuotation(c))
                        {
                        }
                        else if (IsNum(c))
                        {

                        }
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
