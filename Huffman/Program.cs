using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HuffMan
{
    class Program
    {
        static void Main(string[] args)
        {
            //fffffffffffffffffffffffffffffffffffffffffffffeeeeeeeeeeeeeeeedddddddddddddccccccccccccbbbbbbbbbaaaaa
            //A A A A A A A A A A A A A A A B B B B B B B C C C C C C D D D D D D E E E E E
            
            Console.WriteLine("Hello World!");

           /*List<bool> lb = new List<bool>();
            lb.Add(true); lb.Add(true); lb.Add(true);
            BitArray ba = new BitArray(lb.ToArray());
            Console.WriteLine(BitArrToString(ba));*/

            /*bool[] boolArr = { false, true, false };
            BitArray bitArr = new BitArray(boolArr);
            Console.WriteLine(bitArr.Length);
            Console.WriteLine();*/

            /*bool[] b = new bool[] { true, false, true, false, true, false, true, false };
            byte value1 = 0b10000001;

            using (BinaryWriter binWriter = new BinaryWriter(File.Open(@"C:/Users/Guille/Desktop/file.bin", FileMode.Create)))
            {
                binWriter.Write(value1);
            }*/

            /*char[] charArr = "111111111111111".ToCharArray();
            Console.WriteLine(charArr);
            BitArray bitArr = ToBitArr(charArr);
            byte[] byteArr = ToByteArr(bitArr);
            foreach (byte byt in byteArr)
                Console.WriteLine(byt.ToString());
            File.WriteAllBytes(@"C:/Users/Guille/Desktop/file", byteArr);*/

            Dictionary<char, int> alphabet = new Dictionary<char, int>();
            foreach (char c in Console.ReadLine())
            {
                if (c != ' ')
                {
                    if (alphabet.ContainsKey(c))
                        alphabet[c] += 1;
                    else
                        alphabet.Add(c, 1);
                }
            }

            List<Node> heap = new List<Node>();
            foreach(KeyValuePair<char, int> kvp in alphabet)
                heap.Add(new Node(kvp.Value, kvp.Key));

            while (heap.Count > 1)
            {
                //heap.OrderBy(node => node.GetValue());
                heap.Sort((n1, n2) => n1.GetValue().CompareTo(n2.GetValue()));
                Node leaf1 = heap[0];
                Node leaf2 = heap[1];
                heap.RemoveRange(0, 2);
                Node sum = new Node(leaf1.GetValue() + leaf2.GetValue());
                sum.SetLeft(leaf1);
                sum.SetRight(leaf2);
                heap.Add(sum);
            }
            if(heap.Count == 1)
            {
                BinaryTree huffman = new BinaryTree(heap[0]);
                huffman.Print();

                Dictionary<char, BitArray> encoded = huffman.Encode();
                Console.WriteLine(encoded.Count);
                encoded.OrderBy(kvp => kvp.Key);
                foreach (KeyValuePair<char, BitArray> kvp in encoded)
                    Console.WriteLine(kvp.Key + ": " + BitArrToString(kvp.Value));
            }

            Console.ReadLine();
        }

        static string BitArrToString(BitArray bitArr)
        {
            string s = "";
            for (int i = 0; i < bitArr.Count; i++)
            {
                if (bitArr[i])
                    s += '1';
                else
                    s += '0';
            }
            return s;
        }

        static BitArray ToBitArr(char[] ca)
        {
            List<bool> lb = new List<bool>();
            foreach(char c in ca)
            {
                if (c == '1')
                    lb.Add(true);
                else if (c == '0')
                    lb.Add(false);
            }
            return new BitArray(lb.ToArray());
        }

        static byte[] ToByteArr(BitArray ba)
        {
            int remainder = ba.Length % 8, count = 0;
            byte[] bya = new byte[((ba.Length - remainder) + remainder == 0 ? 0 : 8)/8];
            byte aux = 0;

            if(remainder != 0)
                aux <<= 8 - remainder;
            while(count != ba.Length)
            {
                if(count%8 == 0 && count != 0)
                {
                    bya[(count / 8) - 1] = aux;
                    aux = 0;
                }
                if (ba[count])
                    aux &= 1;
                aux <<= 1;
                count++;
            }

            return bya;
        }
    }
}
