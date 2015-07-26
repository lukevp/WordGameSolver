using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGameSolver
{
    public class LetterGrid
    {
        private List<List<char>> grid = null;
        private WordTree tree = null;
                
        public LetterGrid(WordTree inTree)
        {
            tree = inTree;
        } 

        public void Update(MainWindow window)
        {
            // TODO: change this to be data-bound on the UI side and just pass an array directly. 
            grid = new List<List<char>>();
            try
            {
                grid.Add(new List<char> { window.Letter00.Text.ToUpperInvariant()[0], window.Letter01.Text.ToUpperInvariant()[0], window.Letter02.Text.ToUpperInvariant()[0], window.Letter03.Text.ToUpperInvariant()[0]});
                grid.Add(new List<char> { window.Letter10.Text.ToUpperInvariant()[0], window.Letter11.Text.ToUpperInvariant()[0], window.Letter12.Text.ToUpperInvariant()[0], window.Letter13.Text.ToUpperInvariant()[0]});
                grid.Add(new List<char> { window.Letter20.Text.ToUpperInvariant()[0], window.Letter21.Text.ToUpperInvariant()[0], window.Letter22.Text.ToUpperInvariant()[0], window.Letter23.Text.ToUpperInvariant()[0]});
                grid.Add(new List<char> { window.Letter30.Text.ToUpperInvariant()[0], window.Letter31.Text.ToUpperInvariant()[0], window.Letter32.Text.ToUpperInvariant()[0], window.Letter33.Text.ToUpperInvariant()[0] });
            }
            catch
            {
                grid = null;
            }
        }
        public bool IsValid
        {
            get
            {
                return grid != null;
            }
        }

        public List<string> Solve()
        {
            if (!this.IsValid)
            {
                return null;
            }
            List<string> foundWords = new List<string>();
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    foundWords.AddRange(SolveCell(x, y));
                }
            }
            // sort returned words by length of words, descending.
            foundWords = (from s in foundWords
             orderby s.Length descending 
             select s).Distinct().ToList();
            return foundWords;
        }

        public List<string> SolveCell(int x, int y, string currentString="", bool[,] usedCells = null)
        {
            List<string> results = new List<string>();
            if (usedCells == null)
            {
                usedCells = new bool[4,4] { { false, false, false, false },
                                            { false, false, false, false },
                                            { false, false, false, false },
                                            { false, false, false, false }};
            }
            usedCells[y, x] = true;
            currentString += grid[y][x];

            for (int xAdd = -1; xAdd <= 1; xAdd++)
            {
                for(int yAdd = -1; yAdd <= 1; yAdd++)
                {
                    int targetX = x + xAdd;
                    int targetY = y + yAdd;
                    // check if cell is outside target range (0-4 for x & y) or cell has already been used in this branch.
                    if (targetX < 0 || targetX >= 4 || targetY < 0 || targetY >= 4 || usedCells[targetY, targetX] == true)
                    {
                        continue;
                    }
                    WordTreeNode currentNode = tree.GetNode(currentString + grid[targetY][targetX]);
                    if (currentNode != null)
                    {
                        if (currentNode.Word != null)
                        {
                            results.Add(currentNode.Word);
                        }
                        if (!currentNode.IsLeaf)
                        {
                            var newCells = new bool[4, 4];
                            for (int i = 0; i < 4; i++)
                                for (int j = 0; j < 4; j++)
                                    newCells[i, j] = usedCells[i, j];
                            results.AddRange(SolveCell(targetX, targetY, currentString, newCells));
                        }
                    }
                }
            }

            return results;            
        }
    }
}
