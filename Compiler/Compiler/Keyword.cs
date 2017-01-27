using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Keyword
    {
        public enum Key
        {
            Void,
            Double,
            Int,
            For,
            If,
            Else,
            While,
            Return,
        }

        public enum KeyType { Conditional, Loop, Statement, Type }

        public static Key GetKeyword(string word)
        {
            Key keyword;
            Enum.TryParse(word, true, out keyword);
            return keyword;
        }

        public static KeyType GetType(Key key)
        {
            KeyType type = KeyType.Conditional;
            switch (key)
            {
                case Key.Void:
                case Key.Double:
                case Key.Int:
                    type = KeyType.Type;
                    break;
                case Key.For:
                case Key.While:
                    type = KeyType.Loop;
                    break;
                case Key.If:
                case Key.Else:
                    type = KeyType.Conditional;
                    break;
                case Key.Return:
                    type = KeyType.Statement;
                    break;
            }
            return type;
        }

    }
}
