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
            return CanMoveCurrent() && CanMoveNew() && CanFigureMove();
        }

        private bool CanFigureMove()
        {
            switch (moveController.CurrentFigure)
            {
                case Figure.whiteKing:
                case Figure.blackKing:
                    return CheckMovesKing();
                case Figure.whiteQueen:
                case Figure.blackQueen:
                    return false;
                case Figure.whiteRook:
                case Figure.blackRook:
                    return false;
                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return false;
                case Figure.whiteKnight:
                case Figure.blackKnight:
                    return CheckMovesKnight();
                case Figure.whitePawn:
                case Figure.blackPawn:
                    return false;
                default:
                    return false;
            }
        }

        private bool CheckMovesKnight()
        {
            return moveController.AbsDifferenceX == Constants.DIF_KNIGHT_X_1 &&
                moveController.AbsDifferenceY == Constants.DIF_KNIGHT_Y_2 ||
                moveController.AbsDifferenceX == Constants.DIF_KNIGHT_X_2 &&
                moveController.AbsDifferenceY == Constants.DIF_KNIGHT_Y_1;
        }

        private bool CheckMovesKing()
        {
            return (moveController.AbsDifferenceX <= Constants.DIF_KING_X) &&
            (moveController.AbsDifferenceY <= Constants.DIF_KING_Y);

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
