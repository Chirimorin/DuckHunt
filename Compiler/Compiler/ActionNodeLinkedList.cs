using Compiler.action_nodes;

namespace Compiler
{
    public class ActionNodeLinkedList
    {
        private ActionNode _startNode;
        public ActionNode StartNode
        {
            get
            {
                return _startNode;
            }
            private set
            {
                _startNode = value;
            }
        }

        public void insertFirst(ActionNode node)
        {
            if (StartNode != null)
            {
                node.Next = StartNode;
                StartNode = node;
            }
            else
            {
                StartNode = node;
            }
        }

        public void insertLast(ActionNode node)
        {
            if (StartNode != null)
            {
                getLast().Next = node;
            }
            else
            {
                StartNode = node;
            }
        }

        public ActionNode getLast()
        {
            ActionNode node = StartNode;
            while (node.Next != null)
            {
                node = node.Next;
            }
            return node;
        }

        public void insertBefore(ActionNode keyNode, ActionNode node)
        {
            if (keyNode == null)
            {
                this.insertLast(node);
            }
            else if (StartNode == keyNode)
            {
                this.insertFirst(node);
            }
            else
            {
                ActionNode previous = null;
                ActionNode current = StartNode;

                while (current != null && !current.Equals(keyNode))
                {
                    previous = current;
                    current = current.Next;
                }

                if (current != null)
                {
                    if (previous != null)
                    {
                        previous.Next = node;
                    }

                    while (node.Next != null)
                    {
                        node = node.Next;
                    }
                    node.Next = current;
                }
            }
        }  
        
        public ActionNode get(int index)
        {
            int count = 0;
            ActionNode node = StartNode;
            while (node != null && count < index)
            {
                node = node.Next;
                count++;
            }
            return node;
        }     

    }
}
