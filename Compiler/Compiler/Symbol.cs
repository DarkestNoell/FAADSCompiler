using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Symbol
    {
        private String Name;
        private String Type;
        private String Attribute;

        public Symbol(String name, String type, String attribute)
        {
            this.Name = name;
            this.Type = type;
            this.Attribute = attribute;
        }
    }
}
