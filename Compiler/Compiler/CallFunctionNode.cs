using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class CallFunctionNode : TreeNode
    {
        private string functionName;
        public CallFunctionNode(string functionName, List<TreeNode> paramNodes)
        {
            this.functionName = functionName;
            foreach (var paramNode in paramNodes)
            {
                AddTreeNode(paramNode);
            }
        }

        public string GetFunctionName()
        {
            return functionName;
        }

    }
}
