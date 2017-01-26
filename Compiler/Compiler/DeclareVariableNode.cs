using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class DeclareVariableNode : TreeNode
    {
        //Maybe we could combine assign and declare nodes and just have a variable that says if it's assigning or declaring?
        public DeclareVariableNode(ValueNode valueNode, VariableNode variableNode)
        {
            AddTreeNode(valueNode);
            AddTreeNode(variableNode);
        }

        public ValueNode GetValueNode()
        {
            return (ValueNode)GetNode(0);
        }

        public VariableNode GetVariableNode()
        {
            return (VariableNode)GetNode(1);
        }
    }
}
