using System;
namespace ChessEngine
{
    public class FigureOnCell
    {
        public Figure CurrentFigure { get; private set; }
        public Cell CurrentCell { get; private set; }

        public FigureOnCell(Figure figure, Cell cell)
        {
            this.figure = figure;
            this.cell = cell;
        }
    }
}
