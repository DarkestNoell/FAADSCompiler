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
                                    throw new Exception("Error: next token cannot be a keyword");
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
                                    throw new Exception("Error: next one is not id or does not have paren after");
                                }

                            }
                            else
                            {
                                //error
                                throw new Exception();
                            }
                        }
                    }
                    else
                    {
                        //error
                        throw new Exception();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
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
                    throw new Exception("Error: not a variable type");
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
                throw new Exception("Error: need bracket");
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
                //if token is a variable declaration or assignment
                if (currentToken.GetType() == EState.Id)
                {
                    Keyword.Key keyword;
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
                                throw new Exception("Error: next token cannot be a keyword");
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
                                    throw new Exception("Error: did not find a  '=' ");
                                }
                            }
                            else
                            {
                                //error next one is not id or does not have paren after
                                throw new Exception("Error: next token was neither a id nor does have a ')' after it ");
                            }

                        }
                        else
                        {
                            //error
                            throw new Exception();
                        }
                    }
                    else
                    {
                        //if it is an assignment or function call (is in context table)
                        Symbol nameToken = currentToken;
                        tokenCounter++;
                        if (tokens[tokenCounter].GetValue().Equals("="))
                        {
                            if (contextList.Any(x => x.GetName().Equals(nameToken.GetValue()) && !x.IsFunction()))
                            {
                                bodyNode.AddTreeNode(ParseAssignVariableNode(nameToken));
                            }
                        }
                        else if (tokens[tokenCounter].GetValue().Equals("("))
                        {
                            if (contextList.Any(x => x.GetName().Equals(nameToken.GetValue()) && x.IsFunction()))
                            {
                                bodyNode.AddTreeNode(ParseCallFunctionNode(nameToken));
                            }
                        }
                    }
                }
            }
            return null;
        }

        //CallFunction Node may need to be made it's own class
        private TreeNode ParseCallFunctionNode(Symbol nameToken)
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
            ValueNode value = new ValueNode();
            VariableNode variable = null; //variable type and variable name
            while (!tokens[tokenCounter].GetValue().Equals(";"))
            {
                if(typeToken.GetType().Equals(EState.Id))
                {
                    var type = Enum.Parse(typeToken.GetType().ToString());
                    variable = new VariableNode(typeToken.GetType(),nameToken.GetValue());
                }
                else if (typeToken.GetType().Equals(EState.Num)) {
                    variable = new VariableNode(typeToken.GetType(), nameToken.GetValue());
                }
                else if (typeToken.GetType().Equals(EState.Space)) {
                    variable = new VariableNode(VariableType.Type., nameToken.GetValue());
                }
                else if (typeToken.GetType().Equals(EState.String)) {
                    variable = new VariableNode(VariableType.Type.String, nameToken.GetValue());
                }
                else if (typeToken.GetType().Equals(EState.Symbol)) {
                    variable = new VariableNode(VariableType.Type., nameToken.GetValue());
                }
                else {
                    //Throw error
                    throw new Exception();
                }
                tokenCounter++;
            }

            tokenCounter++;
            //Value node, and a variable node
            return new DeclareVariableNode(value,variable);
        }

        //Assuming we found a stored context (example: abc)
        private AssignVariableNode ParseAssignVariableNode(Symbol nameToken)
        {
            //create  Node called variable node and return it
            //parse a variable node here is the variable and find the node for it.
            //value you are asising to and variable node which is the variable name.
            //the variable name "a"
                //
            //the "=" 
                //
            //value type;
            var token = nameToken;
            string name = "Alex";
            ValueNode valnode = new ValueNode();
            VariableNode varnode = new VariableNode(VariableType.Type.String,name);


            return new AssignVariableNode(valnode,varnode);
        }

    }
}
