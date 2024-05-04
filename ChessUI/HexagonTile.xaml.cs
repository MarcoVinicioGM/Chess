using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Chesslogic;

namespace ChessUI
{
    public class HexagonTile : ContentControl
    {
        static HexagonTile()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HexagonTile), new FrameworkPropertyMetadata(typeof(HexagonTile)));
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
                return null;

            Path hexagonPath = new Path
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                Data = Geometry.Parse("M 0.5,28.87 L 25,43.3 L 50,28.87 L 50,0 L 25,14.43 L 0.5,0 Z")
            };

            ContentControl contentPresenter = new ContentControl
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Content = this.Content
            };
            Grid hexagonGrid = new Grid
            {
                Children = { hexagonPath, contentPresenter }
            };

            return hexagonGrid;
        }
    }
}