using System;
using Project__ChessGame.chess;
using Project__ChessGame.chessboard;
using Project__ChessGame.chessboard.Enums;

namespace Project__ChessGame
{
    class Screen
    {

        public static void PrintChessboard(Chessboard chessboard)
        {
            for (int i = 0; i < chessboard.Rows; i++)
            {
                Console.Write(8 - i + "   ");
                for (int j = 0; j < chessboard.Columns; j++)
                {
                    if (chessboard.ChessPiece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        PrintChessPiece(chessboard.ChessPiece(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("    A B C D E F G H");
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(column, row);
        }

        public static void PrintChessPiece(ChessPiece chessPiece)
        {
            if (chessPiece.Color == Color.White)
            {
                Console.Write(chessPiece);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(chessPiece);
                Console.ForegroundColor = aux;
            }
        }

    }
}
