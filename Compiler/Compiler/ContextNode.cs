using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class ContextNode : TreeNode
    {
        private string variableName;
        public ContextNode(string variableName)
        {
            this.variableName = variableName;
        }

        public string GetVariableName()
        {
            return variableName;
        }
    }
}
