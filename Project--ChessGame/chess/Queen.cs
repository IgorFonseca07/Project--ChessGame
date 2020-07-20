using Project__ChessGame.chessboard;
using Project__ChessGame.chessboard.Enums;

namespace Project__ChessGame.chess
{
    class Queen : ChessPiece
    {
        public Queen(Color color, Chessboard chessboard) : base(color, chessboard)
        {
        }

        public override string ToString()
        {
            return "Q";
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
            while (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
                if (Chessboard.ChessPiece(position) != null && Chessboard.ChessPiece(position).Color != Color)
                {
                    break;
                }
                position.Row = position.Row - 1;
            }

            // Right
            position.SetValues(Position.Row, Position.Column + 1);
            while (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
                if (Chessboard.ChessPiece(position) != null && Chessboard.ChessPiece(position).Color != Color)
                {
                    break;
                }
                position.Column = position.Column + 1;
            }

            // Down
            position.SetValues(Position.Row + 1, Position.Column);
            while (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
                if (Chessboard.ChessPiece(position) != null && Chessboard.ChessPiece(position).Color != Color)
                {
                    break;
                }
                position.Row = position.Row + 1;
            }

            // Left
            position.SetValues(Position.Row, Position.Column - 1);
            while (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
                if (Chessboard.ChessPiece(position) != null && Chessboard.ChessPiece(position).Color != Color)
                {
                    break;
                }
                position.Column = position.Column - 1;
            }

            // Up-Left
            position.SetValues(Position.Row - 1, Position.Column - 1);
            while (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
                if (Chessboard.ChessPiece(position) != null && Chessboard.ChessPiece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row - 1, position.Column - 1);
            }

            // Up-Right
            position.SetValues(Position.Row - 1, Position.Column + 1);
            while (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
                if (Chessboard.ChessPiece(position) != null && Chessboard.ChessPiece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row - 1, position.Column + 1);
            }

            // Down-Right
            position.SetValues(Position.Row + 1, Position.Column + 1);
            while (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
                if (Chessboard.ChessPiece(position) != null && Chessboard.ChessPiece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row + 1, position.Column + 1);
            }

            // Down-Left
            position.SetValues(Position.Row + 1, Position.Column - 1);
            while (Chessboard.ValidPosition(position) && CanMove(position))
            {
                match[position.Row, position.Column] = true;
                if (Chessboard.ChessPiece(position) != null && Chessboard.ChessPiece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row + 1, position.Column - 1);
            }

            return match;
        }
    }
}