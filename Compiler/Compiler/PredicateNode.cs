using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class PredicateNode : TreeNode
    {
        //Predicate node needs to have 3 nodes coming off. 
        //Not taking ands and ors into account yet, but when it is it will probably be in the IfNode or LoopNode
        // value/context, condition, value/context
        
        public PredicateNode(ValueNode value1, ConditionNode condition, ValueNode value2)
        {
            AddTreeNode(value1);
            AddTreeNode(condition);
            AddTreeNode(value2);
        }

        public ValueNode GetValueNode1()
        {
            return (ValueNode)GetNode(0);
        }

        public ConditionNode GetConditionNode()
        {
            return (ConditionNode)GetNode(1);
        }

        public ValueNode GetValueNode2()
        {
            return (ValueNode)GetNode(2);
        }
    }
}
