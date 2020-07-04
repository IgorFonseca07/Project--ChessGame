﻿using Project__ChessGame.chess;
using Project__ChessGame.chessboard;
using Project__ChessGame.chessboard.Enums;
using System;

namespace Project__ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Chessboard chessboard = new Chessboard(8, 8);

                chessboard.ChessPiecePosition(new Rook(Color.Black, chessboard), new Position(0, 0));
                chessboard.ChessPiecePosition(new Rook(Color.Black, chessboard), new Position(1, 3));
                chessboard.ChessPiecePosition(new King(Color.Black, chessboard), new Position(0, 2));

                Screen.PrintChessboard(chessboard);
            }
            catch (ChessboardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
