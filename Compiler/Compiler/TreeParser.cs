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
        private int tokenCounter = 0;
        private TreeNode globalNode;

        private List<Context> contextList;
        public TreeParser(List<Symbol> tokens)
        {
            this.tokens = tokens;
            globalNode = new TreeNode();
            contextList = new List<Context>();
        }

        //We will use this to set pre-loaded methods
        private void InitContexts()
        {
            contextList.Add(new Context("WriteLine", VariableNode.Type.Void, true));
        }

        //This is where it starts. Attach all nodes to global node declared above. ^
        public void ParseTree()
        {
            try
            {
                List<Symbol> currentTokens = new List<Symbol>();
                while (tokenCounter != tokens.Count)
                {
                    Symbol currentToken = tokens[tokenCounter];
                    if (currentToken.GetType() == EState.Id)
                    {
                        Keyword.Key keyword;
                        //Need to update this to take case into consideration?
                        if (Enum.TryParse(currentToken.GetValue(), true, out keyword))
                        {
                            if (Keyword.GetType(keyword) == Keyword.KeyType.Type)
                            {
                                //Could make a function for this. Might be easier to have it accessed by the whole class...
                                currentTokens.Add(currentToken);
                                tokenCounter++;
                                currentToken = tokens[tokenCounter];
                                Keyword.Key holder;
                                if (Enum.TryParse(currentToken.GetValue(), true, out holder))
                                {
                                    //Error next one can't be keyword
                                }
                                else if (currentToken.GetType() == EState.Id)
                                {
                                    tokenCounter++;
                                    if (currentTokens[tokenCounter].GetValue().Equals("("))
                                    {
                                        globalNode.AddTreeNode(ParseDeclareFunction(tokens[0], currentToken));
                                    }
                                    else if (currentTokens[tokenCounter].GetValue().Equals("="))
                                    {
                                        globalNode.AddTreeNode(ParseDeclareVariableNode(tokens[0], currentToken));
                                    }
                                }
                                else
                                {
                                    //error next one is not id or does not have paren after
                                }

                            }
                            else
                            {
                                //error
                            }
                        }
                    }
                    else
                    {
                        //error
                    }
                }
            }
            catch (Exception e)
            {
                
            }

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
