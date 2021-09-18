using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HuffMan
{
    class BinaryTree
    {
        Node root;

        public BinaryTree() =>
            root = null;
        public BinaryTree(Node node) =>
            root = node;

        public bool HasRoot() => root != null;

        public void Print() =>
            Print(root, "  ");

        private void Print(Node node, string spaces)
        {
            if (node != null)
            {
                Print(node.GetLeft(), spaces + "    ");
                Console.Write(spaces);
                Console.WriteLine(node.GetChar() + "({0})", node.GetValue());
                Print(node.GetRight(), spaces + "    ");
            }
        }

        public void Insert(int data, char c) =>
            root = Insert(root, data, c);

        private Node Insert(Node node, int data, char c)
        {
            //Console.WriteLine(c + " " + data);
            if (node != null)
            {
                if (node.GetValue() < data)
                    node.SetRight(this.Insert(node.GetRight(), data, c));
                else if (node.GetValue() > data)
                    node.SetLeft(this.Insert(node.GetLeft(), data, c));
            }
            else
                node = new Node(data, c);
            return node;
        }

        public Dictionary<char, BitArray> Encode()
        {
            List<bool> list = new List<bool>();
            return EncodeAux(root, list);
        }

        static private Dictionary<char, BitArray> EncodeAux(Node current, List<bool> list)
        {
            Dictionary<char, BitArray> encoded = new Dictionary<char, BitArray>();
            if (!current.IsLeafNode())
            {
                list.Add(false);
                Dictionary<char, BitArray> aux = EncodeAux(current.GetLeft(), list);

                foreach (KeyValuePair<char, BitArray> kvp in aux)
                    encoded.Add(kvp.Key, kvp.Value);

                list.RemoveAt(list.Count - 1);
                list.Add(true);
                aux = EncodeAux(current.GetRight(), list);

                foreach (KeyValuePair<char, BitArray> kvp in aux)
                    encoded.Add(kvp.Key, kvp.Value);
                list.RemoveAt(list.Count - 1);
            }
            else
                encoded.Add(current.GetChar(), new BitArray(list.ToArray()));
            
            return encoded;
        }
    }
}
