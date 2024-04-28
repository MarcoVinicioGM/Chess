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
        public StartMenu()
        {
            InitializeComponent();
        }

        private void ExpChess(object sender, RoutedEventArgs e)
        {
            OptionSelected.Invoke(StartMenuOption.ExpChess);
        }

        private void FourChess(object sender, RoutedEventArgs e)
        {
            OptionSelected.Invoke(StartMenuOption.FourChess);
        }

        private void RegChess(object sender, RoutedEventArgs e)
        {
            OptionSelected.Invoke(StartMenuOption.RegChess);
        }
    }
}
