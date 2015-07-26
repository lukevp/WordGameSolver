using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WordGameSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WordTree Words { get; set; }

        public LetterGrid InputGrid { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            Words = new WordTree(@"word-lists\main-list.txt");
            Words.PrintTree("tree-representation.txt");

            InputGrid = new LetterGrid(Words);
        }

        public void MoveNext()
        {
            TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;
            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
                if ((Keyboard.FocusedElement as TextBox) != null)
                {
                    (Keyboard.FocusedElement as TextBox).SelectAll();
                }
            }
        }

        private void LetterKeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.A) || (e.Key > Key.Z))
            {
                return;
            }
            var tb = sender as TextBox;
            if (tb.Text.Length == tb.MaxLength)
            {
                this.MoveNext();
            }
            InputGrid.Update(this);
        }
        
        private void LetterFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }


        private void SolveGrid_Click(object sender, RoutedEventArgs e)
        {
            if (!InputGrid.IsValid)
            {
                MessageBox.Show("Please enter one letter into each grid cell before solving grid.");
                return;
            }
            List<string> solution = InputGrid.Solve();
            var text = "";
            foreach (var word in solution)
            {
                text += word + " - " + Game.ScoreWord(word).ToString() + "\n";
            }
            SolutionText.Text = text;
        }
    }
}
