using Compiler.action_nodes;
using Compiler.virtual_machine.Commands;
using System;
using System.Collections.Generic;

namespace Compiler.virtual_machine
{
    public class VirtualMachine
    {
        public string ReturnValue { get; set; }
        public Dictionary<string, string> Variables { get; set; }
        private Dictionary<string, ICommand> Commands { get; set; }

        public VirtualMachine()
        {
            Commands = new Dictionary<string, ICommand>()
            {
                { "Plus", new PlusCommand() },
                { "Minus", new MinusCommand() },
                { "ConstantToReturn", new ConstantToReturnCommand() },
                { "ReturnToVariable", new ReturnToVariableCommand() },
                { "VariableToReturn", new VariableToReturnCommand() },
                { "Print", new PrintCommand() },
                { "Equals", new EqualsCommand() },
                { "NotEquals", new NotEqualsCommand() },
                { "GreaterEquals", new GreaterEqualsCommand() },
                { "SmallerEquals", new SmallerEqualsCommand() },
                { "GreaterThan", new GreaterThanCommand() },
                { "SmallerThan", new SmallerThanCommand() },
            };

        }

        public void Run(ActionNodeLinkedList list)
        {
            ActionNode currentNode = list.StartNode;
            NextNodeVisitor nextNodeVisitor = new NextNodeVisitor(this);
            Variables = new Dictionary<string, string>();

            while (currentNode != null)
            {
                // execute command
                currentNode.Execute(this);

                currentNode.Accept(nextNodeVisitor);
                currentNode = nextNodeVisitor.NextNode;
            }
        }

        public ICommand GetCommand(string command)
        {
            ICommand result;
            if (Commands.TryGetValue(command, out result))
            {
                return result;
            }
            throw new Exception("Command " + command + " not found");
        }
    }
}
