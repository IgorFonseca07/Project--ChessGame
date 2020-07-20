using Project__ChessGame.chessboard;
using Project__ChessGame.chessboard.Enums;

namespace Project__ChessGame.chess
{
    class Knight : ChessPiece
    {
        public Knight(Color color, Chessboard chessboard) : base(color, chessboard)
        {
        }

        public override string ToString()
        {
            return "H";
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

            position.SetValues(Position.Row - 1, Position.Column - 2);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }

            position.SetValues(Position.Row - 2, Position.Column - 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }

            position.SetValues(Position.Row - 2, Position.Column + 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }

            position.SetValues(Position.Row - 1, Position.Column + 2);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }

            position.SetValues(Position.Row + 1, Position.Column + 2);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }

            position.SetValues(Position.Row + 2, Position.Column + 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }

            position.SetValues(Position.Row + 2, Position.Column - 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }

            position.SetValues(Position.Row + 1, Position.Column - 2);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
            }
            return match;

        }

    }
}
