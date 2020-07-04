using System;
using Project__ChessGame.chessboard;

namespace Project__ChessGame
{
    class Screen
    {

        public static void PrintChessboard(Chessboard chessboard)
        {
            for (int i = 0; i < chessboard.Rows; i++)
            {
                for (int j = 0; j < chessboard.Columns; j++)
                {
                    if (chessboard.ChessPiece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(chessboard.ChessPiece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
