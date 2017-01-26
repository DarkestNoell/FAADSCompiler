using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class AssignVariableNode : TreeNode
    {
        public AssignVariableNode(ValueNode valueNode, VariableNode variableNode)
        {
            AddTreeNode(valueNode);
            AddTreeNode(variableNode);
        }

        public ValueNode GetValueNode()
        {
            return (ValueNode) GetNode(0);
        }

        public VariableNode GetVariableNode()
        {
            return (VariableNode) GetNode(1);
        }
    }
}
