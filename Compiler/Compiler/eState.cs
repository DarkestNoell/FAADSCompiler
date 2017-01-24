using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public enum EState
    {
        Symbol,
        String,
        Num,
        Space,
        Id
    };
}
