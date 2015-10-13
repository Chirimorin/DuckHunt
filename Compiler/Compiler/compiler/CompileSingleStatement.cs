using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.action_nodes;

namespace Compiler.compiler
{
    public class CompileSingleStatement : BaseCompiler
    {
        public override void compile(ref Token currentToken, Token endToken, ActionNodeLinkedList nodes, ActionNode before)
        {
            DirectFunctionCall directFunctionCall = new DirectFunctionCall();

            switch (currentToken.TokenType)
            {
                case Tokens.EllipsisOpen:
                    new CompileCondition().compile(ref currentToken, endToken, nodes, before);
                    break;

                case Tokens.Identifier:
                    Token next = currentToken.Next;
                    
                    if (next != null && next.TokenType == Tokens.EllipsisOpen)
                    {
                        //string tempName = getNextLocalVariableName();
                        
                        directFunctionCall.setSize(2);
                        //directFunctionCall.addParameter(0, CompileCondition.GET_FROM_RT);
                        //directFunctionCall.addParameter(1, tempName);
                        //ActionNode beforeFunction = nodes.insertBefore(before, directFunctionCall);
                        nodes.insertBefore(before, directFunctionCall);

                        FunctionCall functionCall = new FunctionCall();
                        functionCall.setSize(2);
                        functionCall.addParameter(0, currentToken.Value);
                        //functionCall.addParameter(1, tempName);
                        nodes.insertBefore(before, functionCall);

                        new CompileCondition().compile(ref currentToken, endToken, nodes, directFunctionCall);
                    }
                    else
                    {
                        directFunctionCall.setSize(2);
                        //directFunctionCall.addParameter(0, SET_CONST_TO_RT);
                        directFunctionCall.addParameter(1, currentToken.Value);
                        nodes.insertBefore(before, directFunctionCall);
                    }
                    break;

                case Tokens.Number:
                    directFunctionCall.setSize(2);
                    //directFunctionCall.addParameter(0, SET_ID_TO_RT);
                    directFunctionCall.addParameter(1, currentToken.Value);
                    nodes.insertBefore(before, directFunctionCall);
                    break;
            }
            
       
        }
    }
}
