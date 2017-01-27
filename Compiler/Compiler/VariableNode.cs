using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class VariableNode : TreeNode
    {
        //Not allowing for custom objects at this time
        

        private VariableType.Type variableType;
        private string variableName;

        public VariableNode(VariableType.Type variableType, string variableName)
        {
            this.variableType = variableType;
            this.variableName = variableName;
        }

        public VariableType.Type GetVariableType()
        {
            return variableType;
        }

        public string GetVariableName()
        {
            return variableName;
        }
    }
}
