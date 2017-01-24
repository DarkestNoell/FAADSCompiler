using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    enum eState
    {
        SYMBOL,
        STRING,
        NUM,
        SPACE,
        ID
    };
}
