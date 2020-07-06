using Project__ChessGame.chessboard;
using Project__ChessGame.chessboard.Enums;

namespace Project__ChessGame.chess
{
    class King : ChessPiece
    {
        public King(Color color, Chessboard chessboard) : base(color, chessboard)
        {
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position position)
        {
            ChessPiece cp = Chessboard.ChessPiece(position);
            return cp == null || cp.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] match = new bool[Chessboard.Rows, Chessboard.Columns];

            Position position = new Position(0, 0);

            // Up
            position.SetValues(Position.Row - 1, Position.Column);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }

            // Diagonal Up-Right
            position.SetValues(Position.Row - 1, Position.Column + 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }

            // Right
            position.SetValues(Position.Row, Position.Column + 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }

            // Diagonal Down-Right
            position.SetValues(Position.Row + 1, Position.Column + 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }

            // Down
            position.SetValues(Position.Row + 1, Position.Column);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }

            // Diagonal Down-Left
            position.SetValues(Position.Row + 1, Position.Column - 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }

            // Left
            position.SetValues(Position.Row, Position.Column - 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }

            // Diagonal Up-Left
            position.SetValues(Position.Row - 1, Position.Column - 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }
            return match;

        }

    }
}
