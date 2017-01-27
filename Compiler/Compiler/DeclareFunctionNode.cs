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
        private VariableType.Type returnType;

        public DeclareFunctionNode(TreeNode paramsNode, TreeNode body, string functionName, VariableType.Type returnType)
        {
            AddTreeNode(paramsNode);
            AddTreeNode(body);
            this.functionName = functionName;
            this.returnType = returnType;
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
