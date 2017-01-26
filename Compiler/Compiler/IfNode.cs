using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class IfNode : TreeNode
    {
        //Might be able to just extend the loop node for this one, but with added functionality maybe that won't work... Very similar tho
        public IfNode(PredicateNode predicateNode, TreeNode body)
        {
            AddTreeNode(predicateNode);
            AddTreeNode(body);
        }

        public PredicateNode GetPredicateNode()
        {
            return (PredicateNode)GetNode(0);
        }

        public TreeNode GetBody()
        {
            return GetNode(1);
        }

        //Thinking we can just use the ifnode as the else node as well. Feel free to change it if it doesn't work out that way.
        public void AddElse(IfNode node)
        {
            AddTreeNode(node);
        }

        public TreeNode GetElse()
        {
            TreeNode elseNode = null;
            if (GetNumberOfChildren() > 2)
            {
                elseNode = GetNode(2);
            }
            return elseNode;
        }
    }
}
