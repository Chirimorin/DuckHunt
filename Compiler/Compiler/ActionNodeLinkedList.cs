using Compiler.action_nodes;

namespace Compiler
{
    public class ActionNodeLinkedList
    {
        public ActionNode StartNode { get; private set; }

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

        public void Add(ActionNode node)
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

        public void Add(ActionNodeLinkedList list)
        {
            Add(list.StartNode);
        }

        public void InsertAfter(ActionNode newNode, ActionNode after)
        {
            if (after != null)
                newNode.Next = after.Next;

            after.Next = newNode;
        }

        public void InsertAfter(ActionNodeLinkedList newNodes, ActionNode after)
        {
            //if (after != null)
                newNodes.LastNode.Next = after.Next;

            after.Next = newNodes.StartNode;
        }

        public ActionNode Get(int index)
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
