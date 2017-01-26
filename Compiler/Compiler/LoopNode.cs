using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class LoopNode : TreeNode
    {
        public LoopNode(PredicateNode predicateNode, TreeNode body)
        {
            AddTreeNode(predicateNode);
            AddTreeNode(body);
        }

        public PredicateNode GetPredicateNode()
        {
            return (PredicateNode) GetNode(0);
        }

        public TreeNode GetBody()
        {
            return GetNode(1);
        }
    }
}
