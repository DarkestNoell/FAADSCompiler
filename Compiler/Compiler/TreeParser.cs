using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            contextList.Add(new Context("WriteLine", VariableType.Type.Void, true));
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
                                    if (tokens[tokenCounter].GetValue().Equals("("))
                                    {
                                        tokenCounter++;
                                        globalNode.AddTreeNode(ParseDeclareFunction(currentTokens[0], currentToken));
                                    }
                                    else if (tokens[tokenCounter].GetValue().Equals("="))
                                    {
                                        tokenCounter++;
                                        globalNode.AddTreeNode(ParseDeclareVariableNode(currentTokens[0], currentToken));
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
            TreeNode paramsNode = new TreeNode();
            TreeNode bodyNode = new TreeNode();

            while (!tokens[tokenCounter].GetValue().Equals(")"))
            {
                Symbol typeToken = tokens[tokenCounter];
                tokenCounter++;
                Symbol nameToken = tokens[tokenCounter];
                tokenCounter++;
                VariableType.Type variableType;
                if (Enum.TryParse(typeToken.GetValue(), true, out variableType))
                {
                    VariableNode variableNode = new VariableNode(variableType, nameToken.GetValue());
                    paramsNode.AddTreeNode(variableNode);
                }
                else
                {
                    //error not a variable type
                }
            }
            tokenCounter++;
            if (tokens[tokenCounter].GetValue().Equals("{"))
            {
                bodyNode = ParseBody();
            }
            else
            {
                //error needs bracket
            }
            DeclareFunctionNode declareFunctionNode = new DeclareFunctionNode(paramsNode, bodyNode, functionNameToken.GetValue(), 
                VariableType.GetVariableTypeFromString(returnTypeToken.GetValue()));
            return declareFunctionNode;
        }

        private TreeNode ParseBody()
        {
            //Currently taking care of declare variable, assign variable and call function;
            //Needs conditionals and loops
            TreeNode bodyNode = new TreeNode();
            tokenCounter++;
            while (!tokens[tokenCounter].GetValue().Equals("}"))
            {
                Symbol currentToken = tokens[tokenCounter];
                //if token is pretty much anything valid since starting with 'blah', 345, or /*( is crap
                if (currentToken.GetType() == EState.Id)
                {
                    Keyword.Key keyword;
                    //if it is a keyword (variable type, condition, loop)
                    if (Enum.TryParse(currentToken.GetValue(), true, out keyword))
                    {
                        //declaring variable
                        if (Keyword.GetType(keyword) == Keyword.KeyType.Type)
                        {
                            //Could make a function for this. Might be easier to have it accessed by the whole class...
                            Symbol variableTypeToken = currentToken;
                            tokenCounter++;
                            Symbol variableNameToken = tokens[tokenCounter];
                            Keyword.Key holder;
                            if (Enum.TryParse(variableNameToken.GetValue(), true, out holder))
                            {
                                //Error variable name can't be keyword
                            }
                            else if (variableNameToken.GetType() == EState.Id)
                            {
                                tokenCounter++;
                                if (tokens[tokenCounter].GetValue().Equals("="))
                                {
                                    tokenCounter++;
                                    bodyNode.AddTreeNode(ParseDeclareVariableNode(variableTypeToken,
                                        variableNameToken));
                                }
                                else
                                {
                                    //error needs =
                                }
                            }
                            else
                            {
                                //error next one is not id or does not have paren after
                            }
                        //else if it is a conditional
                        }
                        else if (currentToken.GetValue().Equals("if"))
                        {
                            bodyNode.AddTreeNode(ParseIfNode());
                        }
                        //else if it is a loop
                        else if (new [] {"while", "for", "do"}.Contains(currentToken.GetValue()))
                        {
                            bodyNode.AddTreeNode(ParseLoopNode());
                        }
                        else
                        {
                            //error
                        }
                    }
                    else //if it is an assignment or function call (is in context table)
                    {
                        Symbol nameToken = currentToken;
                        tokenCounter++;
                        //if it is an assignment
                        if (tokens[tokenCounter].GetValue().Equals("="))
                        {
                            if (contextList.Any(x => x.GetName().Equals(nameToken.GetValue()) && !x.IsFunction()))
                            {
                                bodyNode.AddTreeNode(ParseAssignVariableNode(nameToken));
                            }
                        }
                        //if it is function call
                        else if (tokens[tokenCounter].GetValue().Equals("("))
                        {
                            if (contextList.Any(x => x.GetName().Equals(nameToken.GetValue()) && x.IsFunction()))
                            {
                                bodyNode.AddTreeNode(ParseCallFunctionNode(nameToken));
                            }
                        }
                        else
                        {
                            //error invalid
                        }
                    }
                }
                else
                {
                    //error invalid
                }
            }
            return null;
        }

        //CallFunction Node may need to be made it's own class
        private TreeNode ParseCallFunctionNode(Symbol nameToken)
        {
            List<TreeNode> paramNodes = new List<TreeNode>();
            while (!tokens[tokenCounter].GetValue().Equals(")"))
            {
                paramNodes.Add(ParseValueOrContextNode());
            }
            
            return new CallFunctionNode(nameToken.GetValue(), paramNodes);
        }
        //assuming we have found if
        private IfNode ParseIfNode()
        {
            PredicateNode predicateNode = null;
            TreeNode bodyNode = null;
            if (tokens[tokenCounter].GetValue().Equals("("))
            {
                tokenCounter++;
                predicateNode = ParsePredicateNode();
                while (!tokens[tokenCounter].GetValue().Equals("}"))
                {
                    bodyNode = ParseBody();
                }
                while (tokens[tokenCounter].GetValue().Equals("else"))
                {
                    //set up elses
                }
            }
            else
            {
                //throw error
            }
            return new IfNode(predicateNode, bodyNode);
        }

        private LoopNode ParseLoopNode()
        {
            PredicateNode predicateNode = null;
            TreeNode bodyNode = null;
            //just going to assume a while loop for now. Will get to the others later.
            if (tokens[tokenCounter].GetValue().Equals("("))
            {
                tokenCounter++;
                predicateNode = ParsePredicateNode();
                while (!tokens[tokenCounter].GetValue().Equals("}"))
                {
                    bodyNode = ParseBody();
                }
            }
            else
            {
                //throw error
            }
            return new LoopNode(predicateNode, bodyNode);
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

        private string[] maths = new[] {"/", "+", "-", "*"};
        // (A * A + i) / (2 * A);
        private ValueNode ParseValueNode()
        {
            ValueNode valueNodeToReturn = null;
            int endParen = 1;
            //1 + 2
            if (tokens[tokenCounter].GetType() == EState.Num)
            {
                int value1 = Int32.Parse(tokens[tokenCounter].GetValue());
                tokenCounter++;
                if (maths.Contains(tokens[tokenCounter].GetValue()))
                {
                    string math = tokens[tokenCounter].GetValue();
                    tokenCounter++;
                    valueNodeToReturn = new ValueNode(new ValueNode(value1), new MathNode(math), ParseValueNode());
                }
                else
                {
                        valueNodeToReturn = new ValueNode(value1);
                }
            }
            else if (tokens[tokenCounter].GetType() == EState.Id)
            {
                string context = tokens[tokenCounter].GetValue();
                tokenCounter++;
                if (maths.Contains(tokens[tokenCounter].GetValue()))
                {
                    string math = tokens[tokenCounter].GetValue();
                    tokenCounter++;
                    valueNodeToReturn = new ValueNode(new ContextNode(context), new MathNode(math), ParseValueNode());
                }
                else
                {
                    valueNodeToReturn = new ValueNode(new ContextNode(context), null, null);
                }
            }
            else if (tokens[tokenCounter].GetValue().Equals("("))
            {
                endParen++;
            }
            else if (tokens[tokenCounter].GetValue().Equals(")"))
            {
                endParen--;
            }
            tokenCounter++;           
            //A + 2

            return valueNodeToReturn;
        }

        //(Abs(A - B) > E)
        private PredicateNode ParsePredicateNode()
        {

            return null;
        }

    }
}
