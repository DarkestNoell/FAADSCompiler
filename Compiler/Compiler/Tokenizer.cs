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
                        tokenizeStringBuild(c);
                        break;
                    case EState.Symbol:
                        tokenizeStringBuild(c);
                        break;
                    case EState.Num:
                        tokenizeStringBuild(c);
                        break;
                    case EState.String:
                        tokenizeStringBuild(c);
                        break;
                    case EState.Id:
                        tokenizeStringBuild(c);
                        break;
                }
            }
            //loop thru each char
                //state machine
        }

        public void tokenizeStringBuild(char c)
        {
            if (IsWhiteSpace(c))
            {
                addToken();
                state = EState.Space;
            }
            else if (IsSymbol(c))
            {
                addToken();
                state = EState.Symbol;
                Symbol token2 = new Symbol(sb.ToString(), state);
                tokens.Add(token2);
            }
            else if (IsLetter(c))
            {
                state = EState.Id;
                sb.Append(c);
            }
            else if (IsQuotation(c))
            {
                state = EState.String;
                addToken();
            }
            else if (IsNum(c))
            {
                state = EState.Num;
                sb.Append(c);
            }
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
