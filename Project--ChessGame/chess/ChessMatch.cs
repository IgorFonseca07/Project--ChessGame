using Project__ChessGame.chessboard;
using Project__ChessGame.chessboard.Enums;

namespace Project__ChessGame.chess
{
    class ChessMatch
    {
        public Chessboard Chessboard { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
        public bool GameOver { get; private set; }

        public ChessMatch()
        {
            Chessboard = new Chessboard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            GameOver = false;
            ChessPiecesPositions();
        }

        public void MakeTheMove(Position origin, Position destiny)
        {
            ChessPiece cp = Chessboard.RemoveChessPiece(origin);
            cp.IncreaseQuantityMovements();
            ChessPiece capturedPiece = Chessboard.RemoveChessPiece(destiny);
            Chessboard.ChessPiecePosition(cp, destiny);
        }

        private void ChessPiecesPositions()
        {
            Chessboard.ChessPiecePosition(new Rook(Color.White, Chessboard), new ChessPosition('C', 1).ToPosition());
            Chessboard.ChessPiecePosition(new Rook(Color.White, Chessboard), new ChessPosition('C', 2).ToPosition());
            Chessboard.ChessPiecePosition(new Rook(Color.White, Chessboard), new ChessPosition('D', 2).ToPosition());
            Chessboard.ChessPiecePosition(new Rook(Color.White, Chessboard), new ChessPosition('E', 2).ToPosition());
            Chessboard.ChessPiecePosition(new Rook(Color.White, Chessboard), new ChessPosition('E', 1).ToPosition());
            Chessboard.ChessPiecePosition(new King(Color.White, Chessboard), new ChessPosition('D', 1).ToPosition());

            Chessboard.ChessPiecePosition(new Rook(Color.Black, Chessboard), new ChessPosition('C', 7).ToPosition());
            Chessboard.ChessPiecePosition(new Rook(Color.Black, Chessboard), new ChessPosition('C', 8).ToPosition());
            Chessboard.ChessPiecePosition(new Rook(Color.Black, Chessboard), new ChessPosition('D', 7).ToPosition());
            Chessboard.ChessPiecePosition(new Rook(Color.Black, Chessboard), new ChessPosition('E', 7).ToPosition());
            Chessboard.ChessPiecePosition(new Rook(Color.Black, Chessboard), new ChessPosition('E', 8).ToPosition());
            Chessboard.ChessPiecePosition(new King(Color.Black, Chessboard), new ChessPosition('D', 8).ToPosition());
        }
    }
}
