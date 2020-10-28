using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine
{
     class BoardController
    {
        private Figure [,] figures;

        public string Fen { get; private set; }
        public Color CurrentMoveColor { get; private set; }
        public bool CanCastlingA1 { get; private set; }
        public bool CanCastlingH1 { get; private set; }
        public bool CanCastlingA8 { get; private set; }
        public bool CanCastlingH8 { get; private set; }
        public Cell Enpassat { get; private set; }
        public int DrawNumber { get; private set; }
        public int MoveNumber { get; private set; }

        public BoardController(string fen)
        {
            this.Fen = fen;
            figures = new Figure[Constants.COUNT_SQUARES, Constants.COUNT_SQUARES];

            Initialise();
        }

        public BoardController Move(MoveController moveController)
        {
            BoardController newBoardController = new BoardController(Fen);
            newBoardController.SetFigureAtCell(moveController.CurrentCell, Figure.none);
            newBoardController.SetFigureAtCell(moveController.NewCell, moveController.CurrentFigure);
            newBoardController.CurrentMoveColor = CurrentMoveColor.FlipColor();
            if (CurrentMoveColor == Color.Black)
                newBoardController.MoveNumber++;
            newBoardController.CreateNewFen();
            return newBoardController;
        }

        public IEnumerable<FigureOnCell> YieldFiguresOnCell()
        {
            foreach (Cell cell in Cell.YieldBoardCells())
            {
                if (GetFigureAtCell(cell).GetColor() == CurrentMoveColor)
                    yield return new FigureOnCell(GetFigureAtCell(cell), cell);
            }
        }

        void CreateNewFen() 
        {
            this.Fen = GetFenFigures() + " " +
            GetFenMoveColor() + " " +
            GetFenCastlingFlags() + " " +
            GetFenEnpassat() + " " +
            GetFenDrawNumber() + " " +
            GetFenMoveNumber();
        }

        private string GetFenEnpassat()
        {
            return this.Enpassat.Name;
        }

        private string GetFenMoveNumber()
        {
            return this.MoveNumber.ToString();
        }

        private string GetFenDrawNumber()
        {
            return this.DrawNumber.ToString();
        }

        private string GetFenCastlingFlags()
        {
            string flags = (CanCastlingA1 ? "Q" : "") +
                (CanCastlingH1 ? "K" : "") +
                (CanCastlingA8 ? "q" : "") +
                (CanCastlingH8 ? "k" : "");
            return flags == "" ? "-" : flags;
        }

        private string GetFenMoveColor()
        {
            return this.CurrentMoveColor == Color.White ? "w" : "b";
        }

        private string GetFenFigures()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = Constants.COUNT_SQUARES - 1; y >= 0; y--)
            {
                for (int x = 0; x < Constants.COUNT_SQUARES; x++)
                    sb.Append(figures[x,y] == Figure.none ? 
                    '1' : (char)figures[x, y]);
                if (y > 0)
                    sb.Append("/");
            }
            string emptyLine = GenerateEmptyLine();
            for (int i = Constants.COUNT_SQUARES; i >= 2; i--)
                sb.Replace(emptyLine.Substring(0, i), i.ToString());
            return sb.ToString();

        }

        private string GenerateEmptyLine()
        {
            string result = string.Empty;
            for (int i = 0; i < Constants.COUNT_SQUARES; i++)
                result += "1";
            return result;
        }

        public Figure GetFigureAtCell(Cell cell)
        {
            if (cell.CheckOnBoard())
                return figures[cell.x, cell.y];
            return Figure.none;
        }

        void SetFigureAtCell(Cell cell, Figure figure)
        {
            if (cell.CheckOnBoard())
                figures[cell.x, cell.y] = figure;
        }

        void Initialise()
        {
            //rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
            //0                                           1 2    3 4 5 indexes
            string[] parts = Fen.Split();
            InitFigures(parts[0]);
            InitMoveColor(parts[1]);
            InitCastlingFlags(parts[2]);
            InitEnpassat(parts[3]);
            InitDrawNumber(parts[4]);
            InitMoveDraw(parts[5]);
            CurrentMoveColor = Color.White;
        }

        private void InitMoveDraw(string v)
        {
            MoveNumber = int.Parse(v);
        }

        private void InitDrawNumber(string v)
        {
            DrawNumber = int.Parse(v);
        }

        private void InitEnpassat(string v)
        {
            Enpassat = new Cell(v);
        }

        private void InitCastlingFlags(string v)
        {
            CanCastlingA1 = v.Contains("Q");
            CanCastlingH1 = v.Contains("K");
            CanCastlingA8 = v.Contains("q");
            CanCastlingH8 = v.Contains("k");
        }

        private void InitMoveColor(string v)
        {
            CurrentMoveColor = v == "b" ? Color.Black : Color.White;
        }

        private void InitFigures(string v)
        {
            for (int j = Constants.COUNT_SQUARES; j >= 2; j--)
                v = v.Replace(j.ToString(), (j - 1).ToString() + "1");
            v = v.Replace('1', (char)Figure.none);
            string[] lines = v.Split('/');
            for (int y = Constants.COUNT_SQUARES - 1; y >= 0; y--)
                for (int x = 0; x < Constants.COUNT_SQUARES; x++)
                    figures[x, y] = (Figure)lines[(Constants.COUNT_SQUARES-1) - y][x];
        }
    }
}
