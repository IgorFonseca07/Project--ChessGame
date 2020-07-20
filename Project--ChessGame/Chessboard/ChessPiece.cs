using Project__ChessGame.chessboard.Enums;

namespace Project__ChessGame.chessboard
{
    abstract class ChessPiece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QuantityMovements { get; set; }
        public Chessboard Chessboard { get; set; }

        public ChessPiece(Color color, Chessboard chessboard)
        {
            Position = null;
            Color = color;
            QuantityMovements = 0;
            Chessboard = chessboard;
        }

        public void IncreaseQuantityMovements()
        {
            QuantityMovements++;
        }

        public void DecrementQuantityMovements()
        {
            QuantityMovements--;
        }

        public bool IsTherePossibleMovements()
        {
            bool[,] array = PossibleMovements();
            for (int i = 0; i < Chessboard.Rows; i++)
            {
                for (int j = 0; j < Chessboard.Columns; j++)
                {
                    if (array[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PossibleMovement(Position position)
        {
            return PossibleMovements()[position.Row, position.Column];
        }

        public abstract bool[,] PossibleMovements();
    }
}
