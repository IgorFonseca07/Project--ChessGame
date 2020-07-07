using Project__ChessGame.chessboard;
using Project__ChessGame.chessboard.Enums;
using System.Collections.Generic;

namespace Project__ChessGame.chess
{
    class ChessMatch
    {
        public Chessboard Chessboard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool GameOver { get; private set; }
        private HashSet<ChessPiece> ChessPieces;
        private HashSet<ChessPiece> RemovedChessPieces;

        public ChessMatch()
        {
            Chessboard = new Chessboard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            GameOver = false;
            ChessPieces = new HashSet<ChessPiece>();
            RemovedChessPieces = new HashSet<ChessPiece>();
            ChessPiecesPositions();
        }

        public void MakeTheMove(Position origin, Position destiny)
        {
            ChessPiece cp = Chessboard.RemoveChessPiece(origin);
            cp.IncreaseQuantityMovements();
            ChessPiece capturedPiece = Chessboard.RemoveChessPiece(destiny);
            Chessboard.ChessPiecePosition(cp, destiny);
            if (capturedPiece != null)
            {
                RemovedChessPieces.Add(capturedPiece);
            }
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

        public HashSet<ChessPiece> RemovedPieces(Color color)
        {
            HashSet<ChessPiece> aux = new HashSet<ChessPiece>();
            foreach (ChessPiece x in RemovedChessPieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<ChessPiece> PiecesOnGame(Color color)
        {
            HashSet<ChessPiece> aux = new HashSet<ChessPiece>();
            foreach (ChessPiece x in ChessPieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(RemovedPieces(color));
            return aux;
        }

        public void NewChessPiecePosition(char column, int row, ChessPiece chessPiece)
        {
            Chessboard.ChessPiecePosition(chessPiece, new ChessPosition(column, row).ToPosition());
            ChessPieces.Add(chessPiece);
        }

        private void ChessPiecesPositions()
        {
            NewChessPiecePosition('C', 1, new Rook(Color.White, Chessboard));
            NewChessPiecePosition('C', 2, new Rook(Color.White, Chessboard));
            NewChessPiecePosition('D', 2, new Rook(Color.White, Chessboard));
            NewChessPiecePosition('E', 2, new Rook(Color.White, Chessboard));
            NewChessPiecePosition('E', 1, new Rook(Color.White, Chessboard));
            NewChessPiecePosition('D', 1, new King(Color.White, Chessboard));

            NewChessPiecePosition('C', 7, new Rook(Color.Black, Chessboard));
            NewChessPiecePosition('C', 8, new Rook(Color.Black, Chessboard));
            NewChessPiecePosition('D', 7, new Rook(Color.Black, Chessboard));
            NewChessPiecePosition('E', 7, new Rook(Color.Black, Chessboard));
            NewChessPiecePosition('E', 8, new Rook(Color.Black, Chessboard));
            NewChessPiecePosition('D', 8, new King(Color.Black, Chessboard));
        }
    }
}
