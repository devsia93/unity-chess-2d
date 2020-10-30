﻿using System;
using System.Collections.Generic;

namespace ChessEngine
{
    public class Chess
    {
        BoardController boardController;
        Moves moves;

        public string Fen 
        {
            get
            {
                return boardController.Fen;
            }
        }

        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            boardController = new BoardController(fen);
            moves = new Moves(boardController);
        }

        Chess(BoardController boardController)
        {
            this.boardController = boardController;
            this.moves = new Moves(boardController);
        }

        public Chess Move(string move)
        {
            MoveController moveController = new MoveController(move);
            if (!moves.CanMove(moveController))
                return this;

            BoardController newBC = boardController.Move(moveController);
            Chess newChessEngine = new Chess(newBC);
            return newChessEngine;
        }

        public char GetPositionFigure(int x, int y)
        {
            Cell cell = new Cell(x, y);
            Figure figure = boardController.GetFigureAtCell(cell);
            return figure == Figure.none ? '.' : (char)figure;
        }

        public IEnumerable<string> YieldValidMoves()
        {
            foreach (FigureOnCell fc in boardController.YieldFiguresOnCell())
                foreach (Cell cell in Cell.YieldBoardCells())
                    foreach(Figure transformation in fc.figure.YieldTransformations(cell))
                    {
                        MoveController mc = new MoveController(fc, cell, transformation);
                        if (moves.CanMove(mc))
                            yield return mc.ToString();
                    }
        }
    }
}

