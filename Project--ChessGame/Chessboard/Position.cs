namespace Project__ChessGame.chessboard
{
    class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Position(int linha, int coluna)
        {
            Row = linha;
            Column = coluna;
        }

        public override string ToString()
        {
            return Row
                + ", "
                + Column;
        }

    }
}
