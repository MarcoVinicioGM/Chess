using Chesslogic;
using System.Windows;
using System.Windows.Controls;

namespace ChessUI
{
    public partial class PromotionMenu : UserControl
    {
        public event Action<PieceType> PieceSelected;
        public PromotionMenu(Player player)
        {
            InitializeComponent();
            QueenW.Source = Images.GetImage(player, PieceType.Queen);
            BishopW.Source = Images.GetImage(player, PieceType.Bishop);
            RookW.Source = Images.GetImage(player, PieceType.Rook);
            KnightW.Source = Images.GetImage(player, PieceType.Knight);
        }
        private void Queen_MouseDown(object sender, RoutedEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Queen);
        }
        private void Bishop_MouseDown(object sender, RoutedEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Bishop);
        }
        private void Rook_MouseDown(object sender, RoutedEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Rook);
        }
        private void Knight_MouseDown(object sender, RoutedEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Knight);
        }
    }
}