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

        public ActionNode LastNode
        {
            get
            {
                ActionNode node = StartNode;
                while (node.Next != null)
                {
                    node = node.Next;
                }
                return node;
            }
        }

        public void add(ActionNode node)
        {
            if (StartNode != null)
            {
                LastNode.Next = node;
            }
            else
            {
                StartNode = node;
            }
        }

        public void add(ActionNodeLinkedList list)
        {
            add(list.StartNode);
        }

        public void insertAfter(ActionNode newNode, ActionNode after)
        {
            if (after != null)
                newNode.Next = after.Next;

            after.Next = newNode;
        }

        public void insertAfter(ActionNodeLinkedList newNodes, ActionNode after)
        {
            //if (after != null)
                newNodes.LastNode.Next = after.Next;

            after.Next = newNodes.StartNode;
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
