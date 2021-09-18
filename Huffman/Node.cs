using System;
using System.Collections.Generic;
using System.Text;

namespace HuffMan
{
    class Node
    {
        private int value;
        private char c;
        private Node left = null, right = null;

        public Node(int value, char c)
        {
            this.value = value;
            this.c = c;
        }
        public Node(int value)
        {
            this.value = value;
            this.c = ' ';
        }

        public bool IsLeafNode() => c != ' ';

        public void SetLeft(Node left) =>
            this.left = left;

        public void SetRight(Node right) =>
            this.right = right;

        public Node GetLeft() => left;

        public Node GetRight() => right;

        public int GetValue() => value;

        public char GetChar() => c;
    }
}
