using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2
{
    public class SimplyLinkedList
    {
        public Node Head { get; set; }
        public int Count { get; set; }

        public void Push(long data)
        {
            var newNodeToAdd = new Node();

            if (Head == null)
            {
                Head = newNodeToAdd;
                Head.Data = data;
                Count++;
                return;
            }

            newNodeToAdd.Next = Head;
            newNodeToAdd.Data = data;
            Head = newNodeToAdd;
            Count++;
        }

        public Node FindNodeFromNode(Node currentNode, long distance)
        {
            for (var i=distance; i>0; i--)
            {
                if (currentNode == null)
                    return null;
                currentNode = currentNode.Next;
            }
            return currentNode;
        }
    }

    public class Node
    {
        public Node Next { get; set; }
        public long Data { get; set; }
    }
}
