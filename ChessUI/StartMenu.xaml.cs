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

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for StartMenu.xaml
    /// </summary>
    public partial class StartMenu : UserControl
    {

        public event Action<StartMenuOption> OptionSelected;

        public string UserInput { get; private set; }


        public StartMenu()
        {
            InitializeComponent();
        }
        private void SaveInputButton_Click(object sender, RoutedEventArgs e)
        {
            UserInput = InputTextBox.Text;
            OptionSelected.Invoke(StartMenuOption.FenBoard);
        }

        private void ExpChess(object sender, RoutedEventArgs e)
        {
            OptionSelected.Invoke(StartMenuOption.ExpChess);       
        }

        private void HordeBoard(object sender, RoutedEventArgs e)
        {
            OptionSelected.Invoke(StartMenuOption.HordeBoard);
        }

        private void RegChess(object sender, RoutedEventArgs e)
        {
            OptionSelected.Invoke(StartMenuOption.RegChess);           
        }
        private void Chess960(object sender, RoutedEventArgs e)
        {
            OptionSelected.Invoke(StartMenuOption.Chess960);
        }
    }
}
