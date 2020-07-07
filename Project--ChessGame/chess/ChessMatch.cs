using Project__ChessGame.chessboard;
using Project__ChessGame.chessboard.Enums;

namespace Project__ChessGame.chess
{
    class ChessMatch
    {
        public Chessboard Chessboard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
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

        public void DoTheMove(Position origin, Position destiny)
        {
            MakeTheMove(origin, destiny);
            Turn++;
            ChangePlayer();
        }

        public void OriginPositionValidate(Position position)
        {
            if (Chessboard.ChessPiece(position) == null)
            {
                throw new ChessboardException("There is no chess piece on this position!");
            }
            if (CurrentPlayer != Chessboard.ChessPiece(position).Color)
            {
                throw new ChessboardException("The origin chess piece is not yours!");
            }
            if (!Chessboard.ChessPiece(position).IsTherePossibleMovements())
            {
                throw new ChessboardException("There is no possible movements to this origin chess piece!");
            }
        }

        public void DestinyPositionValidate(Position origin, Position destiny)
        {
            if (!Chessboard.ChessPiece(origin).CanMoveTo(destiny))
            {
                throw new ChessboardException("Invalid destiny position!");
            }
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
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
