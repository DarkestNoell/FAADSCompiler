using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class TreeParser
    {
        private List<Symbol> tokens;
        private TreeNode globalNode;
        public TreeParser(List<Symbol> tokens)
        {
            this.tokens = tokens;
            globalNode = new TreeNode();
        }

        //This is where it starts. Attach all nodes to global node declared above. ^
        public void ParseTree()
        {
               
        }

        //At this point we are assuming we found something like void Main(
        private DeclareFunctionNode ParseDeclareFunction(Symbol returnTypeToken, Symbol functionNameToken)
        {

            return null;
        }

        private TreeNode ParseBody()
        {

            return null;
        }
        //assuming we have found if(
        private IfNode ParseIfNode()
        {

            return null;
        }

        private LoopNode ParseLoopNode()
        {

            return null;
        }

        //assuming we found int abc
        private DeclareVariableNode ParseDeclareVariableNode(Symbol typeToken, Symbol nameToken)
        {

            return null;
        }

        //Assuming we found a stored context (example: abc)
        private AssignVariableNode ParseAssignVariableNode(Symbol nameToken)
        {

            return null;
        }
    }
}
