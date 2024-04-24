
namespace Chesslogic
{
    public class PositionDirection
    {
        public readonly static PositionDirection Up = new PositionDirection(-1, 0);
        public readonly static PositionDirection Down = new PositionDirection(1, 0);
        public readonly static PositionDirection Left = new PositionDirection(0, 1);
        public readonly static PositionDirection Right = new PositionDirection(0, -1);
        public readonly static PositionDirection UpLeft = Up + Left;
        public readonly static PositionDirection UpRight = Up + Right;
        public readonly static PositionDirection DownLeft = Down + Left;
        public readonly static PositionDirection DownRight = Down + Right;

        public int RowChange { get; }
        public int ColumnChange { get; }

        public PositionDirection(int rowChange, int columnChange)
        {
            RowChange = rowChange;
            ColumnChange = columnChange;
        }

        public static PositionDirection operator +(PositionDirection dir1, PositionDirection dir2)
        {
            return new PositionDirection(dir1.RowChange + dir2.RowChange, dir1.ColumnChange + dir2.ColumnChange);
        }
        public static PositionDirection operator *(int scale, PositionDirection dir)
        {
            return new PositionDirection(scale * dir.RowChange, scale * dir.ColumnChange);
        }
    }
}
