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

            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.GameOver)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.OriginPositionValidate(origin);

                        bool[,] possiblePositions = match.Chessboard.ChessPiece(origin).PossibleMovements();

                        Console.Clear();
                        Screen.PrintChessboard(match.Chessboard, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadChessPosition().ToPosition();
                        match.DestinyPositionValidate(origin, destiny);

                        match.DoTheMove(origin, destiny);
                    }
                    catch (ChessboardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            
            }

            catch (ChessboardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
