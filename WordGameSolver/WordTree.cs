using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGameSolver
{
    public class WordTree
    {
        public WordTreeNode RootNode { get; }

        public WordTree(string wordFilePath)
        {
            this.RootNode = new WordTreeNode();

            var words = File.ReadAllLines(wordFilePath);
            foreach(var word in words)
            {
                this.AddWord(word.ToUpperInvariant());
            }
        }

        public void PrintTree(string outputPath)
        {
            File.WriteAllText(outputPath, PrintNode(RootNode));
        }
        public string PrintNode(WordTreeNode node, string pastNodes="", int tabCount=0)
        {
            string retVal = "";
            if (node.IsLeaf)
            {
                retVal += new String('\t', tabCount) + node.Word + "#\n";
            }
            else
            {
                retVal += new String('\t', tabCount) + pastNodes;
                if (node.Word != null)
                {
                    retVal += "*\n";
                }
                else
                {
                    retVal += "\n";
                }
                foreach (var child in node.Nodes)
                {
                    retVal += PrintNode(child.Value, pastNodes + child.Key, tabCount + 1);
                }
            }
            return retVal;
        }


        public void AddWord(string word, int index=0, WordTreeNode currentNode=null)
        {
            if (currentNode == null) currentNode = RootNode;

            if (index == word.Length)
            {
                currentNode.Word = word;
                return;
            }
            else
            {
                if (!currentNode.Nodes.ContainsKey(word[index]))
                {
                    currentNode.Nodes[word[index]] = new WordTreeNode();
                }
                currentNode = currentNode.Nodes[word[index]];
                AddWord(word, index + 1, currentNode);
            }
        }

        public WordTreeNode GetNode(string query)
        {
            WordTreeNode currentNode = this.RootNode;
            foreach (var c in query)
            {
                if (currentNode.Nodes.ContainsKey(c))
                {
                    currentNode = currentNode.Nodes[c];
                }
                else
                {
                    currentNode = null;
                    break;
                }
            }
            return currentNode;
        }
    }

    
    public class WordTreeNode
    {
        public string Word { get; set; }

        public WordTreeNode()
        {
            Nodes = new SortedDictionary<char, WordTreeNode>();
        }
        
        public bool IsLeaf
        { 
            get
            {
                return Nodes.Count == 0;
            }
        }
        

        public SortedDictionary<char, WordTreeNode> Nodes { get; set; }
    }
}
