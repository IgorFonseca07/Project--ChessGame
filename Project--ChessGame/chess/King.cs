using Project__ChessGame.chessboard;
using Project__ChessGame.chessboard.Enums;

namespace Project__ChessGame.chess
{
    class King : ChessPiece
    {
        private ChessMatch Game;

        public King(Color color, Chessboard chessboard, ChessMatch game) : base(color, chessboard)
        {
            Game = game;
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

        private bool TestRookToCastle(Position position)
        {
            ChessPiece cp = Chessboard.ChessPiece(position);
            return cp != null && cp is Rook && cp.Color == Color && cp.QuantityMovements == 0;
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

            // #SpecialMove Castle
            if (QuantityMovements == 0 && !Game.Check)
            {
                // #SpecialMove Small Castle
                Position posR1 = new Position(Position.Row, Position.Column + 3);
                if (TestRookToCastle(posR1))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Chessboard.ChessPiece(p1) == null && Chessboard.ChessPiece(p2) == null)
                    {
                        match[Position.Row, Position.Column + 2] = true;
                    }
                }
                // #SpecialMove Big Castle
                Position posR2 = new Position(Position.Row, Position.Column - 4);
                if (TestRookToCastle(posR2))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Chessboard.ChessPiece(p1) == null && Chessboard.ChessPiece(p2) == null && Chessboard.ChessPiece(p3) == null)
                    {
                        match[Position.Row, Position.Column - 2] = true;
                    }
                }
            }

            return match;

        }

    }
}
