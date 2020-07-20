using Project__ChessGame.chessboard;
using Project__ChessGame.chessboard.Enums;

namespace Project__ChessGame.chess
{
    class Pawn : ChessPiece
    {
        public Pawn(Color color, Chessboard chessboard) : base(color, chessboard)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool IsThereEnemy(Position position)
        {
            ChessPiece cp = Chessboard.ChessPiece(position);
            return cp != null && cp.Color != Color;
        }

        private bool Free(Position position)
        {
            return Chessboard.ChessPiece(position) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] match = new bool[Chessboard.Rows, Chessboard.Columns];

            Position position = new Position(0, 0);

            if (Color == Color.White)
            {
                position.SetValues(Position.Row - 1, Position.Column);
                if (Chessboard.ValidPosition(position) && Free(position))
                {
                    match[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row - 2, Position.Column);
                if (Chessboard.ValidPosition(position) && Free(position) && QuantityMovements == 0)
                {
                    match[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row - 1, Position.Column - 1);
                if (Chessboard.ValidPosition(position) && IsThereEnemy(position))
                {
                    match[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row - 1, Position.Column + 1);
                if (Chessboard.ValidPosition(position) && IsThereEnemy(position))
                {
                    match[position.Row, position.Column] = true;
                }
            }
            else
            {
                position.SetValues(Position.Row + 1, Position.Column);
                if (Chessboard.ValidPosition(position) && Free(position))
                {
                    match[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row + 2, Position.Column);
                if (Chessboard.ValidPosition(position) && Free(position) && QuantityMovements == 0)
                {
                    match[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row + 1, Position.Column - 1);
                if (Chessboard.ValidPosition(position) && IsThereEnemy(position))
                {
                    match[position.Row, position.Column] = true;
                }
                position.SetValues(Position.Row + 1, Position.Column + 1);
                if (Chessboard.ValidPosition(position) && IsThereEnemy(position))
                {
                    match[position.Row, position.Column] = true;
                }
            }
            return match;
        }
    }
}