using Chesslogic;
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
using Chesslogic;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for GameOverMenu.xaml
    /// </summary>
    public partial class GameOverMenu : UserControl
    {

        public event Action<Option> OptionSelected;
        public GameOverMenu(Game gameState)
        {
            InitializeComponent();

            Result result = gameState.Result;
            WinnerText.Text = GetWinnerText(result.Winner);
            ReasonText.Text = GetReasonText(result.Reasonforend, gameState.Current);
        }

        private static string GetWinnerText(Player winner)
        {
            return winner switch
            {
                Player.White => "White Wins",
                Player.Black => "Black Wins",
                _ => "Draw"
            };
        }

        private static string PlayerString(Player player)
        {
            return player switch
            {
                Player.White => "White",
                Player.Black => "Black",
                _ => ""
            };
        }

        private static string GetReasonText(Endreason reason, Player currentPlayer)
        {
            return reason switch
            {
                Endreason.Stalemate => $"Stalemate - {PlayerString(currentPlayer)} Can't Move",
                Endreason.Checkmate => $"Checkmate - {PlayerString(currentPlayer)} Can't Move",
                Endreason.FiftyMoveRule => $"Fifty-Move Rule",
                Endreason.InsufficientMaterial => "Insufficient Material",
                Endreason.ThreefoldRepetition => "Threefold Repetition",
                _ => ""

            };
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected.Invoke(Option.Exit);
        }
    }
}
