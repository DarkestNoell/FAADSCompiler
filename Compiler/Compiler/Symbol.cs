using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Symbol
    {
        private string name;
        private string type;

        public Symbol(string name, string type)
        {
            this.name = name;
            this.type = type;
        }
    }
}
