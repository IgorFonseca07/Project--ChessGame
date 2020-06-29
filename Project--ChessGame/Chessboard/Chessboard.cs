using System.Reflection.Metadata.Ecma335;

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
    }
}
