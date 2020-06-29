using Project__ChessGame.chessboard.Enums;

namespace Project__ChessGame.chessboard
{
    class ChessPiece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QuantityMovements { get; set; }
        public Chessboard Chessboard { get; set; }

        public ChessPiece(Position position, Color color, Chessboard chessboard)
        {
            Position = position;
            Color = color;
            QuantityMovements = 0;
            Chessboard = chessboard;
        }
    }
}
