using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datacomHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            String line = readFile();

            ArrayList List = new ArrayList();
            ArrayList steps = new ArrayList();

            var result = line.GroupBy(c => c).Where(c => c.Any()).Select(c => new { charName = c.Key, charCount = c.Count() }).ToList(); //  LINQ for message.txt

            #region filling List with result of LINQ
            for (int i = 0; i < result.Count; i++)
            {
                Node node = new Node(result[i].charName.ToString(), result[i].charCount);
                node.probability = (double)(node.freq) / (double)(line.Length); // Calculating Probability
                List.Add(node);
                Console.WriteLine("Symbol : " + node.name + "   Frequency : " + node.freq + "    Probability : " + node.probability);
            }
            Console.WriteLine();
            #endregion

            shannonEntropy(List); // Calculating Shannon Entropy

            int stepcounter = 1;
            while (List.Count >= 2)
            {
                Node min;
                for (int i = 0; i < 2; i++)
                {
                    Node temp;
                    min = (Node)List[0];
                    for (int j = 0; j < List.Count; j++)
                    {
                        temp = (Node)List[j];
                        if (temp.name != min.name)
                        {
                            if (temp.freq < min.freq)
                            {
                                min = temp;
                            }
                        }

                    }
                    steps.Add(min);//    Adding deleted node to steps
                    List.Remove(min); // Deleting minumun value from List
                }

                Node newnode = new Node((Node)steps[steps.Count - 2], (Node)steps[steps.Count - 1]); //         Creating newnode
                ((Node)steps[steps.Count - 2]).parent = newnode;//  Parent child relations
                ((Node)steps[steps.Count - 1]).parent = newnode;////////////////////////

                steps.Add(newnode);
                List.Add(newnode);

                Console.WriteLine("Step : " + stepcounter); // Printing Each Step
                for (int i = 0; i < List.Count; i++)
                {
                    Console.WriteLine(((Node)List[i]).name + ((Node)List[i]).freq);

                }
                Console.WriteLine();
                stepcounter++;
            }
            Console.WriteLine("----- Huffman Codes"); Console.WriteLine();
            Print((Node)List[0]);// Tree traversal & setting binarys & Printing binarys

            double expectedCodeLenght = codeLenght((Node)List[0]); // Calculating Expected code lenght
            Console.WriteLine(); Console.WriteLine("Expected Code Lenght ");
            Console.WriteLine(expectedCodeLenght);
            Console.ReadKey();

        }

        public static void Print(Node node)//       Recursive method for tree
        {
            if (node.left != null)
            {
                node.left.binary = node.binary + "0";
                Print(node.left);
            }
            if (node.right != null)
            {
                node.right.binary = node.binary + "1";
                Print(node.right);
            }
            if (node.left == null || node.right == null)
            {
                Console.WriteLine(node.name + node.freq + "       " + node.binary);
            }
        }

        public static void shannonEntropy(ArrayList List) //  Calculating shannon entropy
        {
            double entropy = 0;
            for (int i = 0; i < List.Count; i++)
            {
                entropy = entropy + (((Node)List[i]).probability * Math.Log(((Node)List[i]).probability, 2));
            }
            entropy = entropy * (-1); Console.WriteLine();
            Console.WriteLine("---- Shannon Entropy ----");
            Console.WriteLine("     " + entropy); Console.WriteLine();
        }

        public static double codeLenght(Node node)
        { // Calculating Expected Code Lenght

            double expected = 0;

            if (node.left != null)
            {
                expected = expected + codeLenght(node.left);
            }
            if (node.right != null)
            {
                expected = expected + codeLenght(node.right);
            }
            if (node.left == null || node.right == null)
            {
                expected = expected + node.binary.Length * node.probability;
            }
            return expected;
        }

        public static string readFile() // File Readng
        {
            string line = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("message.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    line = sr.ReadToEnd();
                    Console.WriteLine("-------- Message ");
                    Console.WriteLine();
                    Console.WriteLine("   " + line);
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return line;
        }
    }

}
