using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datacomHomework
{
    class Node
    {
        public string name;
        public int freq;
        public Node parent;
        public Node left;
        public Node right;
        public string binary;
        public double probability;

        public Node(string name, int freq)
        {
            this.name = name;
            this.freq = freq;
            parent = null;
            left = null;
            right = null;
            binary = "";
        }
        public Node(Node node1, Node node2)
        {
            this.name = node1.name+node2.name;
            this.freq = node1.freq+node2.freq;
            parent = null;
            left = node1;
            right = node2;
            binary = "";
        }
    }
}
