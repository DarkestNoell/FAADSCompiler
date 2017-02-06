using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class ValueNode : TreeNode
    {
        //we could simplify this to just be a double since that would fit everything for the Square Root Program
        private object value;

        public ValueNode(object value)
        {
            this.value = value;
        }

        //TreeNodes var1 and var2 can be ValueNodes or ContextNode
        public ValueNode(TreeNode var1Node, MathNode mathNode, TreeNode var2Node)
        {
            AddTreeNode(var1Node);
            AddTreeNode(mathNode);
            AddTreeNode(var2Node);
        }

        public object GetValue()
        {
            return value;
        }

        public TreeNode GetVar1Node()
        {
            return GetNode(0);
        }

        public TreeNode GetVar2Node()
        {
            return GetNode(2);
        }

        public MathNode GetMathNode()
        {
            return (MathNode)GetNode(1);
        }
    }
}
