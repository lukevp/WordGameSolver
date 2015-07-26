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
        public List<List<char>> Grid = new List<List<char>>();
        public MainWindow()
        {
            InitializeComponent();

            Words = new WordTree(@"word-lists\main-list.txt");
            Words.PrintTree("tree-representation.txt");
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
        }
        
        private void LetterFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }


        private void SolveGrid_Click(object sender, RoutedEventArgs e)
        {
            Grid = new List<List<char>>();
            try
            { 
                Grid.Add(new List<char> { Letter00.Text[0], Letter01.Text[0], Letter02.Text[0], Letter03.Text[0] });
                Grid.Add(new List<char> { Letter10.Text[0], Letter11.Text[0], Letter12.Text[0], Letter13.Text[0] });
                Grid.Add(new List<char> { Letter20.Text[0], Letter21.Text[0], Letter22.Text[0], Letter23.Text[0] });
                Grid.Add(new List<char> { Letter30.Text[0], Letter31.Text[0], Letter32.Text[0], Letter33.Text[0] });
            }
            catch
            {
                MessageBox.Show("Please enter one letter into each grid cell before solving grid.");
                return;
            }
        }
    }
}
