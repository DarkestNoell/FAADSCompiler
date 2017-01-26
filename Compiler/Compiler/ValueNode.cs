using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class ValueNode : TreeNode
    {
        //we could simplify this to just be a double since that would fit everything for the Square Root Program
        private object value;

        public object GetValue()
        {
            return value;
        }
    }
}
