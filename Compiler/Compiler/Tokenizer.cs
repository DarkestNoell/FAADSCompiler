using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Tokenizer
    { //shalom

        private List<Symbol> tokens = new List<Symbol>();
        private StringBuilder sb = new StringBuilder();
        private EState state = EState.Space;
        public void Tokenize(string instructions)
        {
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
                            sb.Append(c);
                            state = EState.Symbol;
                            if (instructions[i+1] == '=')
                            {
                                sb.Append(c);
                            }
                            addToken();
                        }
                        else if (IsLetter(c))
                        {
                            sb.Append(c);
                            state = EState.Id;
                        }
                        else if (IsQuotation(c))
                        {
                            sb.Append(c);
                            state = EState.String;
                        }else if (IsNum(c))
                        {
                            sb.Append(c);
                            state = EState.Num;
                        }

                        break;
                    case EState.Symbol:
                        if (IsWhiteSpace(c))
                        {
                            //No need to make token here because symbol token is made above
                            state = EState.Space;
                            sb.Clear();
                        }
                        else if (IsLetter(c))
                        {
                            //example (myVar)
                            state = EState.Id;
                            sb.Append(c);
                        }
                        else if (IsSymbol(c))
                        {
                            //<= already taken into consideration. Cannot happen here. This will be a new symbol
                            sb.Append(c);
                            addToken();
                        }
                        else if (IsQuotation(c))
                        {
                            //example ("hello")
                            state = EState.String;
                            sb.Append(c);
                        }

                        break;
                    case EState.Num:
                        if (IsWhiteSpace(c))
                        {
                            addToken();
                            state = EState.Space;
                        }
                        else if (IsSymbol(c))
                        {
                            //symbol can't be in a number unless it's a '.'
                            if (c == '.')
                            {
                                sb.Append(c);
                            }
                            else
                            {
                                addToken();
                                sb.Append(c);
                                state = EState.Symbol;
                            }
                        }
                        else if (IsLetter(c))
                        {
                            //The only thing I can think of for valid num is a float like 3.2f, but we aren't using those so meh
                            addToken();
                            //save it as an id even though it makes no sense? idk...
                            sb.Append(c);
                            state = EState.Id;
                        }
                        else if (IsQuotation(c))
                        {
                            //this one also makes no sense 1"abvc"?
                            addToken();
                            //save it as a string even though it makes no sense? idk...
                            sb.Append(c);
                            state = EState.Id;
                        }
                        else if (IsNum(c))
                        {
                            sb.Append(c);
                        }
                        break;
                    case EState.String:
                        if (IsWhiteSpace(c))
                        {
                            //spaces are ok when in a string
                            sb.Append(c);
                        }
                        else if (IsQuotation(c))
                        {
                            sb.Append(c);
                            addToken();
                            state = EState.Space;
                        }
                        else if (IsSymbol(c))
                        {
                            sb.Append(c);
                        }
                        else if (IsLetter(c))
                        {
                            sb.Append(c);
                        }
                        else if (IsNum(c))
                        {
                            sb.Append(c);
                        }
                        break;
                    case EState.Id:
                        if (IsWhiteSpace(c))
                        {
                            addToken();
                            state = EState.Space;
                        }
                        else if (IsSymbol(c))
                        {
                            //example abc_de
                            if (c == '_')
                            {
                                sb.Append(c);
                            }
                            else
                            {
                                //otherwise it's a new symbol. Example: (abc);
                                addToken();
                                //Need to think of a better way to handle these symbols.
                                //For now. Hax
                                i--;
                                state = EState.Space;
                            }
                        }
                        else if (IsLetter(c))
                        {
                            sb.Append(c);
                        }
                        else if (IsQuotation(c))
                        {
                            //abc"db and abc "hello" does not work parse error?
                        }
                        else if (IsNum(c))
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            //loop thru each char
                //state machine
        }

        public void addToken()
        {
            if (sb.Length > 0)
            {
                Symbol token = new Symbol(sb.ToString(), state);
                tokens.Add(token);
                sb.Clear();
            }
        }
        public bool IsWhiteSpace(char c)
        {
            return c == ' ';
        }
        public bool IsSymbol(char c)
        {
            return char.IsSymbol(c);
        }
        public bool IsNum(char c)
        {
            return char.IsNumber(c);
        }

        public bool IsQuotation(char c)
        {
            return c == '"';
        }
        public bool IsLetter(char c)
        {
            return char.IsLetter(c);
        }
    }
}
