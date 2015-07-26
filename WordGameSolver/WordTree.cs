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
                this.AddWord(word);
            }
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
    }

    
    public class WordTreeNode
    {
        public string Word { get; set; }

        public WordTreeNode()
        {
            Nodes = new Dictionary<char, WordTreeNode>();
        }
        
        public bool IsLeaf
        { 
            get
            {
                return Nodes.Count == 0;
            }
        }

        public Dictionary<char, WordTreeNode> Nodes { get; set; }
    }
}
