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
        public bool Check { get; private set; }
        private HashSet<ChessPiece> ChessPieces;
        private HashSet<ChessPiece> RemovedChessPieces;
        public ChessPiece VulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            Chessboard = new Chessboard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            GameOver = false;
            Check = false;
            ChessPieces = new HashSet<ChessPiece>();
            RemovedChessPieces = new HashSet<ChessPiece>();
            VulnerableEnPassant = null;
            ChessPiecesPositions();
        }

        public ChessPiece MakeTheMove(Position origin, Position destiny)
        {
            ChessPiece cp = Chessboard.RemoveChessPiece(origin);
            cp.IncreaseQuantityMovements();
            ChessPiece capturedPiece = Chessboard.RemoveChessPiece(destiny);
            Chessboard.ChessPiecePosition(cp, destiny);
            if (capturedPiece != null)
            {
                RemovedChessPieces.Add(capturedPiece);
            }

            // #SpecialMove Small Castle
            if (cp is King && destiny.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Row, origin.Column + 3);
                Position destinyR = new Position(origin.Row, origin.Column + 1);
                ChessPiece r = Chessboard.RemoveChessPiece(originR);
                r.IncreaseQuantityMovements();
                Chessboard.ChessPiecePosition(r, destinyR);
            }

            // #SpecialMove Big Castle
            if (cp is King && destiny.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Row, origin.Column - 4);
                Position destinyR = new Position(origin.Row, origin.Column - 1);
                ChessPiece r = Chessboard.RemoveChessPiece(originR);
                r.IncreaseQuantityMovements();
                Chessboard.ChessPiecePosition(r, destinyR);
            }

            // #SpecialMove En Passant
            if (cp is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == null)
                {
                    Position posP;
                    if(cp.Color == Color.White)
                    {
                        posP = new Position(destiny.Row + 1, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(destiny.Row - 1, destiny.Column);
                    }
                    capturedPiece = Chessboard.RemoveChessPiece(posP);
                    RemovedChessPieces.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destiny, ChessPiece capturedPiece)
        {
            ChessPiece cp = Chessboard.RemoveChessPiece(destiny);
            cp.DecrementQuantityMovements();
            if (capturedPiece != null)
            {
                Chessboard.ChessPiecePosition(capturedPiece, destiny);
                RemovedChessPieces.Remove(capturedPiece);
            }
            Chessboard.ChessPiecePosition(cp, origin);

            // #SpecialMove Small Castle
            if (cp is King && destiny.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Row, origin.Column + 3);
                Position destinyR = new Position(origin.Row, origin.Column + 1);
                ChessPiece r = Chessboard.RemoveChessPiece(destinyR);
                r.DecrementQuantityMovements();
                Chessboard.ChessPiecePosition(r, originR);
            }

            // #SpecialMove Big Castle
            if (cp is King && destiny.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Row, origin.Column - 4);
                Position destinyR = new Position(origin.Row, origin.Column - 1);
                ChessPiece r = Chessboard.RemoveChessPiece(destinyR);
                r.DecrementQuantityMovements();
                Chessboard.ChessPiecePosition(r, originR);
            }

            // #SpecialMove En Passant
            if (cp is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == VulnerableEnPassant)
                {
                    ChessPiece pawn = Chessboard.RemoveChessPiece(destiny);
                    Position posP;
                    if (cp.Color == Color.White)
                    {
                        posP = new Position(3, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(4, destiny.Column);
                    }
                    Chessboard.ChessPiecePosition(pawn, posP);
                }
            }
        }

        public void DoTheMove(Position origin, Position destiny)
        {
            ChessPiece capturedPiece = MakeTheMove(origin, destiny);
            if (IsOnCheck(CurrentPlayer))
            {
                UndoMove(origin, destiny, capturedPiece);
                throw new ChessboardException("You cannot put yourself on check!");
            }

            ChessPiece cp = Chessboard.ChessPiece(destiny);

            // #SpecialMove Promotion
            if (cp is Pawn)
            {
                if ((cp.Color == Color.White && destiny.Row == 0) || (cp.Color == Color.Black && destiny.Row == 7))
                {
                    cp = Chessboard.RemoveChessPiece(destiny);
                    ChessPieces.Remove(cp);
                    ChessPiece queen = new Queen(cp.Color, Chessboard);
                    Chessboard.ChessPiecePosition(queen, destiny);
                    ChessPieces.Add(queen);
                }
            }

            if (IsOnCheck(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (IsOnCheckmate(Opponent(CurrentPlayer)))
            {
                GameOver = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }

            // #SpecialMove En Passant
            if (cp is Pawn && (destiny.Row == origin.Row - 2 || destiny.Row == origin.Row + 2))
            {
                VulnerableEnPassant = cp;
            }
            else
            {
                VulnerableEnPassant = null;
            }
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
            if (!Chessboard.ChessPiece(origin).PossibleMovement(destiny))
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

        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private ChessPiece King(Color color)
        {
            foreach (ChessPiece x in PiecesOnGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool IsOnCheck(Color color)
        {
            ChessPiece K = King(color);
            if (K == null)
            {
                throw new ChessboardException("There is no " + color + " king on the chessboard!");
            }
            foreach (ChessPiece x in PiecesOnGame(Opponent(color)))
            {
                bool[,] array = x.PossibleMovements();
                if (array[K.Position.Row, K.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsOnCheckmate(Color color)
        {
            if (!IsOnCheck(color))
            {
                return false;
            }
            foreach (ChessPiece x in PiecesOnGame(color))
            {
                bool[,] array = x.PossibleMovements();
                for (int i = 0; i < Chessboard.Rows; i++)
                {
                    for (int j = 0; j < Chessboard.Columns; j++)
                    {
                        if (array[i, j])
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            ChessPiece capturedPiece = MakeTheMove(origin, destiny);
                            bool check = IsOnCheck(color);
                            UndoMove(origin, destiny, capturedPiece);
                            if (!check)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void NewChessPiecePosition(char column, int row, ChessPiece chessPiece)
        {
            Chessboard.ChessPiecePosition(chessPiece, new ChessPosition(column, row).ToPosition());
            ChessPieces.Add(chessPiece);
        }

        private void ChessPiecesPositions()
        {
            NewChessPiecePosition('A', 1, new Rook(Color.White, Chessboard));
            NewChessPiecePosition('B', 1, new Knight(Color.White, Chessboard));
            NewChessPiecePosition('C', 1, new Bishop(Color.White, Chessboard));
            NewChessPiecePosition('D', 1, new Queen(Color.White, Chessboard));
            NewChessPiecePosition('E', 1, new King(Color.White, Chessboard, this));
            NewChessPiecePosition('F', 1, new Bishop(Color.White, Chessboard));
            NewChessPiecePosition('G', 1, new Knight(Color.White, Chessboard));
            NewChessPiecePosition('H', 1, new Rook(Color.White, Chessboard));
            NewChessPiecePosition('A', 2, new Pawn(Color.White, Chessboard, this));
            NewChessPiecePosition('B', 2, new Pawn(Color.White, Chessboard, this));
            NewChessPiecePosition('C', 2, new Pawn(Color.White, Chessboard, this));
            NewChessPiecePosition('D', 2, new Pawn(Color.White, Chessboard, this));
            NewChessPiecePosition('E', 2, new Pawn(Color.White, Chessboard, this));
            NewChessPiecePosition('F', 2, new Pawn(Color.White, Chessboard, this));
            NewChessPiecePosition('G', 2, new Pawn(Color.White, Chessboard, this));
            NewChessPiecePosition('H', 2, new Pawn(Color.White, Chessboard, this));

            NewChessPiecePosition('A', 8, new Rook(Color.Black, Chessboard));
            NewChessPiecePosition('B', 8, new Knight(Color.Black, Chessboard));
            NewChessPiecePosition('C', 8, new Bishop(Color.Black, Chessboard));
            NewChessPiecePosition('D', 8, new Queen(Color.Black, Chessboard));
            NewChessPiecePosition('E', 8, new King(Color.Black, Chessboard, this));
            NewChessPiecePosition('F', 8, new Bishop(Color.Black, Chessboard));
            NewChessPiecePosition('G', 8, new Knight(Color.Black, Chessboard));
            NewChessPiecePosition('H', 8, new Rook(Color.Black, Chessboard));
            NewChessPiecePosition('A', 7, new Pawn(Color.Black, Chessboard, this));
            NewChessPiecePosition('B', 7, new Pawn(Color.Black, Chessboard, this));
            NewChessPiecePosition('C', 7, new Pawn(Color.Black, Chessboard, this));
            NewChessPiecePosition('D', 7, new Pawn(Color.Black, Chessboard, this));
            NewChessPiecePosition('E', 7, new Pawn(Color.Black, Chessboard, this));
            NewChessPiecePosition('F', 7, new Pawn(Color.Black, Chessboard, this));
            NewChessPiecePosition('G', 7, new Pawn(Color.Black, Chessboard, this));
            NewChessPiecePosition('H', 7, new Pawn(Color.Black, Chessboard, this));
        }
    }
}
