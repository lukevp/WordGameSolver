using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGameSolver
{
    public static class Game
    {
        private static Dictionary<char, int> letterValues = new Dictionary<char, int>
        {
            { 'A', 1 },
            { 'B', 4 },
            { 'C', 4 },
            { 'D', 2 },
            { 'E', 1 },
            { 'F', 4 },
            { 'G', 3 },
            { 'H', 4 },
            { 'I', 1 },
            { 'J', 10 },
            { 'K', 5 },
            { 'L', 1 },
            { 'M', 3 },
            { 'N', 1 },
            { 'O', 1 },
            { 'P', 4 },
            { 'Q', 10 },
            { 'R', 1 },
            { 'S', 1 },
            { 'T', 1 },
            { 'U', 2 },
            { 'V', 4 },
            { 'W', 4 },
            { 'X', 8 },
            { 'Y', 4 },
            { 'Z', 10 }
        };
        public static int ScoreWord(string word)
        {
            int score = 0;
            foreach (var c in word)
            {
                score += letterValues[c];
            }
            if (word.Length >= 9)
            {
                score += 25;
            }
            else if (word.Length >= 5 && word.Length < 9)
            {
                score += (word.Length - 4) * 5;
            }
            return score;
        }
    }
}
