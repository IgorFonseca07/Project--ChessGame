namespace Project__ChessGame.chessboard
{
    class Chessboard
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private ChessPiece[,] ChessPieces;

        public Chessboard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            ChessPieces = new ChessPiece[rows, columns];
        }

        public ChessPiece ChessPiece(int row, int column)
        {
            return ChessPieces[row, column];
        }

        public ChessPiece ChessPiece(Position position)
        {
            return ChessPieces[position.Row, position.Column];
        }

        public bool ChessPieceExists(Position position)
        {
            PositionValidate(position);
            return ChessPiece(position) != null;
        }

        public void ChessPiecePosition(ChessPiece chessPiece, Position position)
        {
            if (ChessPieceExists(position))
            {
                throw new ChessboardException("There is a chess piece on this position already!");
            }
            ChessPieces[position.Row, position.Column] = chessPiece;
            chessPiece.Position = position;
        }

        public ChessPiece RemoveChessPiece(Position position)
        {
            if (ChessPiece(position) == null)
            {
                return null;
            }
            ChessPiece aux = ChessPiece(position);
            aux.Position = null;
            ChessPieces[position.Row, position.Column] = null;
            return aux;
        }

        public bool ValidPosition(Position position)
        {
            if (position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void PositionValidate(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new ChessboardException("Invalid position!");
            }
        }
    }
}
