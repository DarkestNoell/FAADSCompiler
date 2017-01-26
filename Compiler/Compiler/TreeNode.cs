using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class TreeNode
    {
        private List<TreeNode> nodes;

        public TreeNode()
        {
            nodes = new List<TreeNode>();
        }

        public TreeNode GetNode(int index)
        {
            return nodes[index];
        }

        public void AddTreeNode(TreeNode node)
        {
            nodes.Add(node);
        }

        public int GetNumberOfChildren()
        {
            return nodes.Count;
        }
    }
}
