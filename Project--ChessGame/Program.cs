using Project__ChessGame.chess;
using Project__ChessGame.chessboard;
using Project__ChessGame.chessboard.Enums;
using System;

namespace Project__ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Chessboard chessboard = new Chessboard(8, 8);

            chessboard.ChessPiecePosition(new Rook(Color.Black, chessboard), new Position(0, 0));
            chessboard.ChessPiecePosition(new Rook(Color.Black, chessboard), new Position(1, 3));
            chessboard.ChessPiecePosition(new King(Color.Black, chessboard), new Position(2, 4));

            Screen.PrintChessboard(chessboard);

            Console.ReadLine();
        }
    }
}
