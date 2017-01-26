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
        public enum Type { Integer, String, Double }

        private Type variableType;
        private string variableName;

        public VariableNode(Type variableType, string variableName)
        {
            this.variableType = variableType;
            this.variableName = variableName;
        }

        public Type GetVariableType()
        {
            return variableType;
        }

        public string GetVariableName()
        {
            return variableName;
        }
    }
}
