using System;
using ChessEngine;

namespace test
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Chess chess = new Chess();
            Console.WriteLine(chess.GetPositionFigure(2,5));
            while (true)
            {
                Console.WriteLine(chess.Fen);
                string move = Console.ReadLine();
                if (move == "")
                    break;
                chess = chess.Move(move);
            }

        }
    }
}
