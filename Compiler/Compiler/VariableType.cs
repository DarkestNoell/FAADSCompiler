using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class VariableType
    {
        public enum Type { Integer, String, Double, Void }

        public static Type GetVariableTypeFromString(string type)
        {
            return (Type)Enum.Parse(typeof(Type), type, true);
        }
    }
}
