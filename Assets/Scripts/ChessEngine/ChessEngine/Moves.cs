using System;
namespace ChessEngine
{
     class Moves
    {
        MoveController moveController;
        BoardController boardController;

        public Moves(BoardController boardController)
        {
            this.boardController = boardController;
        }

        public bool CanMove(MoveController mc)
        {
            this.moveController = mc;
            return CanMoveCurrent() && CanMoveNew();
        }

         bool CanMoveCurrent()
        {
            return moveController.CurrentCell.CheckOnBoard() &&
                moveController.CurrentFigure.GetColor() == 
                boardController.CurrentMoveColor && boardController
                .GetFigureAtCell(moveController.CurrentCell) == 
                moveController.CurrentFigure;
        }

         bool CanMoveNew()
        {
            return moveController.NewCell.CheckOnBoard() &&
                boardController.GetFigureAtCell(moveController.NewCell)
                .GetColor() != boardController.CurrentMoveColor;
        }
    }
}
