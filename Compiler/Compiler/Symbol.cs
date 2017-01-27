using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Symbol
    {
        private string value;
        private EState type;

        public Symbol(string value, EState type)
        {
            this.value = value;
            this.type = type;
        }

        public override string ToString()
        {
            return String.Format("Value: {0}, Type: {1}", value, type);
        }

        public string GetValue()
        {
            return value;
        }

        public EState GetType()
        {
            return type;
        }
    }
}
