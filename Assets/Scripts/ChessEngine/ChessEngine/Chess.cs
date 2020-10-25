using System;
namespace ChessEngine
{
    public class Chess
    {
        public string Fen { get; private set; }
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.Fen = fen;
        }

        public Chess Move(string move)
        {
            Chess newChessEngine = new Chess(Fen);
            return newChessEngine;
        }

        public string GetPositionFigure(int x, int y)
        {
            Cell cell = new Cell(x, y);
            string name = cell.Name;
            return name;
        }

    }
}

