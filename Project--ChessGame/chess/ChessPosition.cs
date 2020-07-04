﻿using Project__ChessGame.chessboard;

namespace Project__ChessGame.chess
{
    class ChessPosition
    {
        public char Column { get; set; }
        public int Row { get; set; }

        public ChessPosition(char column, int row)
        {
            Column = column;
            Row = row;
        }

        public Position ToPosition()
        {
            return new Position(8 - Row, Column - 'A');
        }

        public override string ToString()
        {
            return "" + Column + Row;
        }
    }
}
