using Project__ChessGame.chessboard;
using Project__ChessGame.chessboard.Enums;

namespace Project__ChessGame.chess
{
    class Pawn : ChessPiece
    {
        private ChessMatch ChessMatch;

        public Pawn(Color color, Chessboard chessboard, ChessMatch chessMatch) : base(color, chessboard)
        {
            ChessMatch = chessMatch;
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

                // #SpecialMove En Passant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Chessboard.ValidPosition(left) && IsThereEnemy(left) && Chessboard.ChessPiece(left) == ChessMatch.VulnerableEnPassant)
                    {
                        match[left.Row - 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Chessboard.ValidPosition(right) && IsThereEnemy(right) && Chessboard.ChessPiece(right) == ChessMatch.VulnerableEnPassant)
                    {
                        match[right.Row - 1, right.Column] = true;
                    }
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

                // #SpecialMove En Passant
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Chessboard.ValidPosition(left) && IsThereEnemy(left) && Chessboard.ChessPiece(left) == ChessMatch.VulnerableEnPassant)
                    {
                        match[left.Row + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Chessboard.ValidPosition(right) && IsThereEnemy(right) && Chessboard.ChessPiece(right) == ChessMatch.VulnerableEnPassant)
                    {
                        match[right.Row + 1, right.Column] = true;
                    }
                }

            }
            return match;
        }
    }
}