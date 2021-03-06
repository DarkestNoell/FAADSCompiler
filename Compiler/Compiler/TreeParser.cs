﻿using System;
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

        public TreeNode GetGlobalNode()
        {
            return globalNode;
        }

        public List<Context> GetContextList()
        {
            return contextList;
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
            VariableType.Type funcVariableType = VariableType.GetVariableTypeFromString(returnTypeToken.GetValue());
            contextList.Add(new Context(functionNameToken.GetValue(), funcVariableType));
            DeclareFunctionNode declareFunctionNode = new DeclareFunctionNode(paramsNode, bodyNode, functionNameToken.GetValue(),
                funcVariableType);
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
                        //else if it is a conditional
                        }
                        else if (currentToken.GetValue().Equals("if"))
                        {
                            bodyNode.AddTreeNode(ParseIfNode());
                        }
                        //else if it is a loop
                        else if (new [] {"while", "for", "do"}.Contains(currentToken.GetValue()))
                        {
                            tokenCounter++;
                            if (tokens[tokenCounter].GetValue().Equals("("))
                            {
                                tokenCounter++;
                                if (currentToken.GetValue().Equals("for"))
                                {
                                    Symbol typeToken = tokens[tokenCounter];
                                    tokenCounter++;
                                    Symbol nameToken = tokens[tokenCounter];
                                    tokenCounter++;
                                    bodyNode.AddTreeNode(ParseDeclareVariableNode(typeToken, nameToken));
                                }
                                bodyNode.AddTreeNode(ParseLoopNode(currentToken));

                            }
                            else
                            {
                                //throw error
                                throw new Exception();
                            }
                        }
                        else
                        {
                            //error
                            throw new Exception();
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
                            throw new Exception("Error: invalid");
                        }
                    }
                }
                else
                {
                    //error invalid
                    throw new Exception("Error: invalid");
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
                paramNodes.Add(ParseValueNode());
            }
            
            return new CallFunctionNode(nameToken.GetValue(), paramNodes);
        }
        //assuming we have found if
        private IfNode ParseIfNode()
        {
            PredicateNode predicateNode = null;
            TreeNode bodyNode = null;
            IfNode ifNode = null;
            if (tokens[tokenCounter].GetValue().Equals("("))
            {
                tokenCounter++;
                predicateNode = ParsePredicateNode();
                while (!tokens[tokenCounter].GetValue().Equals("}"))
                {
                    bodyNode = ParseBody();
                }
                ifNode = new IfNode(predicateNode, bodyNode);
                while (tokens[tokenCounter].GetValue().Equals("else"))
                {
                    if (tokens[tokenCounter].GetValue().Equals("{"))
                    {
                        tokenCounter++;
                        ifNode.AddTreeNode(ParseBody());
                    }
                    else
                    {
                        Console.WriteLine("ERror!");
                    }
                }
            }
            else
            {
                //throw error
                throw new Exception();
            }
            return ifNode;
        }

        private LoopNode ParseLoopNode(Symbol loopType)
        {
            PredicateNode predicateNode = null;
            TreeNode bodyNode = null;
            //just going to assume a while loop for now. Will get to the others later.
                tokenCounter++;
                predicateNode = ParsePredicateNode();

                if (loopType.GetValue().Equals("for"))
                {
                    AssignVariableNode assignVariableNode = ParseAssignVariableNode(tokens[tokenCounter]);
                    bodyNode = ParseBody();
                    bodyNode.AddTreeNode(assignVariableNode);
                }
                else
                {
                    bodyNode = ParseBody();
                }

            return new LoopNode(predicateNode, bodyNode);
        }

        //assuming we found int abc
        private DeclareVariableNode ParseDeclareVariableNode(Symbol typeToken, Symbol nameToken)
        {
           // ValueNode value = new ValueNode();
            VariableNode variable = null; //variable type and variable name
            ValueNode value = null;
            if (!tokens[tokenCounter].GetValue().Equals(";"))
            {
                    variable = new VariableNode(VariableType.GetVariableTypeFromString(typeToken.GetValue()), nameToken.GetValue());
                    value = ParseValueNode();
            }
            else {
                //Throw error
                throw new Exception("ERROR: token is not of type declare variable node ");
            }
            tokenCounter++;
            contextList.Add(new Context(nameToken.GetValue(), VariableType.GetVariableTypeFromString(typeToken.GetValue()), false));
            //Value node, and a variable node
            return new DeclareVariableNode(value,variable);
        }

        //Assuming we found a stored context (example: abc)
        private AssignVariableNode ParseAssignVariableNode(Symbol nameToken)
        {
            //Value node
            ValueNode valnode = null;
            VariableNode varnode = null;
            //the variable name "a"
                //
            //the "=" 
                //
            //value type;
            
            if(nameToken.GetType().Equals(EState.Id) && !tokens[tokenCounter].GetValue().Equals(";"))
              {
                varnode = new VariableNode(VariableType.Type.String,tokens[tokenCounter].GetValue());
                tokenCounter++; 
              }
            else if (nameToken.GetType().Equals(EState.Num) && !tokens[tokenCounter].GetValue().Equals(";"))
              {

                valnode = ParseValueNode();
                tokenCounter++; 
              }

            return new AssignVariableNode(valnode,varnode);
        }

        private string[] maths = new[] {"/", "+", "-", "*"};
        //This method needs a refactor. Will not cover all senarios quite right
        // (A * A + i) / (2 * A);
        private ValueNode ParseValueNode()
        {
            ValueNode valueNodeToReturn = null;

            if (tokens[tokenCounter].GetValue().Equals("("))
            {
                tokenCounter++;
            }
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
                Symbol contextToken = tokens[tokenCounter];
                string context = contextToken.GetValue();
                tokenCounter++;
                ContextNode contextNode = new ContextNode(context);
                if (IsFunction(context))
                {
                    if (tokens[tokenCounter].GetValue().Equals("("))
                    {
                        tokenCounter++;
                        contextNode.AddTreeNode(ParseCallFunctionNode(contextToken));
                    }
                    else
                    {
                        //throw error
                    }

                }
                if (maths.Contains(tokens[tokenCounter].GetValue()))
                {
                    string math = tokens[tokenCounter].GetValue();
                    tokenCounter++;
                    valueNodeToReturn = new ValueNode(contextNode, new MathNode(math), ParseValueNode());
                }
                else
                {
                    valueNodeToReturn = new ValueNode(contextNode, null, null);
                }
            }
            tokenCounter++;
            //get past closed parens
            while (tokens[tokenCounter].GetValue().Equals(")"))
            {
                tokenCounter++;
            }
            //get past ;
            tokenCounter++;
            return valueNodeToReturn;
        }

        private bool IsFunction(string context)
        {
            return contextList.Exists(x => x.GetName().Equals(context) && x.IsFunction());
        }

        //(Abs(A - B) > E)
        private PredicateNode ParsePredicateNode()
        {
            ValueNode val1 = ParseValueNode();
            ConditionNode conditionNode = new ConditionNode(tokens[tokenCounter].GetValue());
            tokenCounter++;
            ValueNode val2 = ParseValueNode();
            return new PredicateNode(val1, conditionNode, val2);
        }

    }
}
