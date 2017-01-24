using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Token
    {
        public Token() { }
        string name { get; set; }

        EState type { get; set; }

    }
}
