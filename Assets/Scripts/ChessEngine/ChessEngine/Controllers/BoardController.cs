using System;
namespace ChessEngine
{
     class BoardController
    {
        private Figure [,] figures;

        public string Fen { get; private set; }
        public Color CurrentMoveColor { get; private set; }

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
            return newBoardController;
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
            SetFigureAtCell(new Cell("a2"), Figure.whitePawn);
            SetFigureAtCell(new Cell("h6"), Figure.blackPawn);
            SetFigureAtCell(new Cell("h7"), Figure.blackPawn);
            CurrentMoveColor = Color.White;
        }
    }
}
