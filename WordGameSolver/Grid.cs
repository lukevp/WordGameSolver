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
        public void Update(MainWindow window)
        {
            // TODO: change this to be data-bound on the UI side and just pass an array directly. 
            grid = new List<List<char>>();
            try
            {
                grid.Add(new List<char> { window.Letter00.Text[0], window.Letter01.Text[0], window.Letter02.Text[0], window.Letter03.Text[0]});
                grid.Add(new List<char> { window.Letter10.Text[0], window.Letter11.Text[0], window.Letter12.Text[0], window.Letter13.Text[0]});
                grid.Add(new List<char> { window.Letter20.Text[0], window.Letter21.Text[0], window.Letter22.Text[0], window.Letter23.Text[0]});
                grid.Add(new List<char> { window.Letter30.Text[0], window.Letter31.Text[0], window.Letter32.Text[0], window.Letter33.Text[0]});
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
                return grid == null;
            }
        }

        public List<string> Solve()
        {
            if (!this.IsValid)
            {
                return null;
            }
            
            return new List<string> { "Hello", "World!" };
        }

    }
}
