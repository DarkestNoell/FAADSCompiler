using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class DeclareFunctionNode :TreeNode
    {
        private string functionName;
        //Should probably move this enum to it's own file or something
        private VariableNode.Type returnType;

        public DeclareFunctionNode(TreeNode paramsNode, TreeNode body)
        {
            AddTreeNode(paramsNode);
            AddTreeNode(body);
        }

        public TreeNode GetBody()
        {
            return GetNode(1);
        }

        //Should this be it's own object or does this work? Idk...
        public TreeNode GetParamsNode()
        {
            return GetNode(0);
        }
    }
}
