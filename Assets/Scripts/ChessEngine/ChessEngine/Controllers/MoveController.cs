using System;
namespace ChessEngine
{
     class MoveController
    {
        public Figure CurrentFigure { get; private set; }
        public Cell CurrentCell { get; private set; }
        public Cell NewCell { get; private set; }
        public Figure Transformation { get; private set; }

        public MoveController(FigureOnCell figureOnCell, Cell newCell, Figure transformation = Figure.none)
        {
            this.CurrentFigure = figureOnCell.figure;
            this.CurrentCell = figureOnCell.CurrentCell;
            this.NewCell = newCell;
            this.Transformation = transformation;
        }

        public MoveController(string move)//example for move values: Pe2e4, Ph7h8Q
        {
            this.CurrentFigure = (Figure)move[0];
            this.CurrentCell = new Cell(move.Substring(1, 2));
            this.NewCell = new Cell(move.Substring(3, 2));
            if (move.Length == 6)
                this.Transformation = (Figure)move[5];
            else this.Transformation = Figure.none;
        }

        public override string ToString()
        {
            return ((char)CurrentFigure).ToString() + CurrentCell.Name + 
            NewCell.Name;
        }
    }
}
