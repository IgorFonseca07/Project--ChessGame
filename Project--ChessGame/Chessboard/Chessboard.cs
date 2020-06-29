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

        public ChessPiece chessPiece(int row, int column)
        {
            return ChessPieces[row, column];
        }

        public void ChessPiecePosition(ChessPiece chessPiece, Position position)
        {
            ChessPieces[position.Row, position.Column] = chessPiece;
            chessPiece.Position = position;
        }
    }
}
