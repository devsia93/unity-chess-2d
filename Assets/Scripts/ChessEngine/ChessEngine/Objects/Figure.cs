using System;
namespace ChessEngine
{
    enum Figure
    {
        none, 
        whiteKing = 'K',
        whiteQueen = 'Q',
        whiteRook = 'R',
        whiteKnight = 'N',
        whitePawn = 'P',
        blackKing = 'k',
        blackQueen = 'q',
        blackRook = 'r',
        blackKnight = 'n',
        blackPawn = 'p'
    }

    static class FigureMethods
    {
        public static Color GetColor(this Figure figure)
        {
            if (figure == Figure.none)
                return Color.None;

            return figure.ToString().ToLower().Contains("white") ?
            Color.White : Color.Black;
        }
    }
}
