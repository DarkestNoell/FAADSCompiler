using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class TreeRunner
    {
        private List<ContextValue> contextValues;
        private TreeParser treeParser;

        public TreeRunner(TreeParser treeParser)
        {
            this.treeParser = treeParser;
            contextValues = new List<ContextValue>();
            treeParser.GetContextList().ForEach(x => contextValues.Add(new ContextValue(x)));
        }

        public void RunTree(TreeParser treeParser)
        {
            ExecuteTreeNode(GetMethodNode(treeParser.GetGlobalNode(), "Main"));
        }

        public void ExecuteTreeNode(TreeNode treeNode)
        {
            if (treeNode.GetType() == typeof(AssignVariableNode))
            {
                AssignVariableNode assignVariableNode = (AssignVariableNode) treeNode;
                //run assign variable
                contextValues.FirstOrDefault(x => x.GetContext().GetName().Equals(assignVariableNode.GetVariableNode().GetVariableName()))
                    .SetValue(ExecuteValueNode(assignVariableNode.GetValueNode()));
            }
            else if (treeNode.GetType() == typeof(CallFunctionNode))
            {
                CallFunctionNode callFunctionNode = (CallFunctionNode) treeNode;
                //run call function
                DeclareFunctionNode declareFunctionNode = GetMethodNode(treeParser.GetGlobalNode(),
                    callFunctionNode.GetFunctionName());
                //TODO
                //set params in contextValues
                //run body
            }
            else if (treeNode.GetType() == typeof(LoopNode))
            {
                LoopNode loopNode = (LoopNode) treeNode;
                double val1 = loopNode.GetPredicateNode().GetValueNode1().GetValue();
                double val2 = loopNode.GetPredicateNode().GetValueNode2().GetValue();
                //TODO - fill out rest of cases
                switch (loopNode.GetPredicateNode().GetConditionNode().GetConditionSymbol())
                {
                    case ConditionNode.ConditionSymbol.Equals:
                        while (val1 == val2)
                        {
                            ExecuteTreeNode(loopNode.GetBody());
                        }
                        break;
                    case ConditionNode.ConditionSymbol.GreaterThan:

                        break;
                    case ConditionNode.ConditionSymbol.GreaterThanOrEqual:

                        break;

                    case ConditionNode.ConditionSymbol.LessThan:

                        break;
                    case ConditionNode.ConditionSymbol.LessThanOrEqual:

                        break;
                    case ConditionNode.ConditionSymbol.NotEquals:

                        break;
                }
            }
            else if (treeNode.GetType() == typeof(IfNode))
            {
                IfNode ifNode = (IfNode) treeNode;
                double val1 = ifNode.GetPredicateNode().GetValueNode1().GetValue();
                double val2 = ifNode.GetPredicateNode().GetValueNode2().GetValue();
                //TODO - fill out rest of cases
                switch (ifNode.GetPredicateNode().GetConditionNode().GetConditionSymbol())
                {
                    case ConditionNode.ConditionSymbol.Equals:
                        if (val1 == val2)
                        {
                            ExecuteTreeNode(ifNode.GetBody());
                        }
                        break;
                    case ConditionNode.ConditionSymbol.GreaterThan:

                        break;
                    case ConditionNode.ConditionSymbol.GreaterThanOrEqual:

                        break;

                    case ConditionNode.ConditionSymbol.LessThan:

                        break;
                    case ConditionNode.ConditionSymbol.LessThanOrEqual:

                        break;
                    case ConditionNode.ConditionSymbol.NotEquals:

                        break;
                }
            }
            else if (treeNode.GetType() == typeof(DeclareVariableNode))
            {
                DeclareVariableNode declareVariableNode = (DeclareVariableNode) treeNode;
                //TODO - add variable to contextValues list
            }
            else //this means it is a body node
            {
                for (int i = 0; i < treeNode.GetNumberOfChildren(); i++)
                {
                    ExecuteTreeNode(treeNode.GetNode(i));
                }
            }
        }

        private double ExecuteValueNode(ValueNode valueNode)
        {
            double value = 0;
            //TODO - get value from doing math and/or retrieving stored contextValues
            return value;
        }

        public DeclareFunctionNode GetMethodNode(TreeNode globalNode, string methodName)
        {
            DeclareFunctionNode node = null;
            for (int i = 0; i < globalNode.GetNumberOfChildren(); i++)
            {
                if (globalNode.GetNode(i).GetType() == typeof(DeclareFunctionNode))
                {
                    DeclareFunctionNode declareFunctionNode = (DeclareFunctionNode)globalNode.GetNode(i);
                    if (declareFunctionNode.GetFunctionName().Equals(methodName))
                    {
                        node = declareFunctionNode;
                    }
                }
            }
            return node;
        }
    }
}
