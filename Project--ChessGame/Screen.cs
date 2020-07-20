using System;
using System.Collections.Generic;
using Project__ChessGame.chess;
using Project__ChessGame.chessboard;
using Project__ChessGame.chessboard.Enums;

namespace Project__ChessGame
{
    class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            PrintChessboard(match.Chessboard);
            Console.WriteLine();
            PrintRemovedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.Turn);
            if (!match.GameOver)
            {
                Console.WriteLine("Move: " + match.CurrentPlayer);
                if (match.Check)
                {
                    Console.WriteLine("CHECK!");
                }
            }
            else
            {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine("Winner: " + match.CurrentPlayer);
            }
        }

        public static void PrintRemovedPieces(ChessMatch match)
        {
            Console.WriteLine("Removed pieces:");
            Console.Write("White: ");
            PrintGroup(match.RemovedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintGroup(match.RemovedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintGroup(HashSet<ChessPiece> group)
        {
            Console.Write("[");
            foreach (ChessPiece x in group)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void PrintChessboard(Chessboard chessboard)
        {
            for (int i = 0; i < chessboard.Rows; i++)
            {
                Console.Write(8 - i + "   ");
                for (int j = 0; j < chessboard.Columns; j++)
                {
                    PrintChessPiece(chessboard.ChessPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("    A B C D E F G H");
        }

        public static void PrintChessboard(Chessboard chessboard, bool[,] possiblePositions )
        {
            ConsoleColor originBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < chessboard.Rows; i++)
            {
                Console.Write(8 - i + "   ");
                for (int j = 0; j < chessboard.Columns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = changedBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originBackground;
                    }
                    PrintChessPiece(chessboard.ChessPiece(i, j));
                    Console.BackgroundColor = originBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("    A B C D E F G H");
            Console.BackgroundColor = originBackground;
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
            if (chessPiece == null)
            {
                Console.Write("- ");
            }
            else
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
                Console.Write(" ");
            }
        }

    }
}
