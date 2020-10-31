using System;
using System.Text;
using ChessEngine;

namespace test
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Chess chess = new Chess("7k/8/5K2/6Q1/8/8/8/8 w - - 0 1");
            Console.WriteLine(chess.GetPositionFigure(2,5));
            while (true)
            {
                Console.WriteLine(chess.Fen);
                Console.WriteLine(ChessToAscii(chess));

                foreach (string moves in chess.YieldValidMoves())
                    Console.WriteLine(moves);

                string move = Console.ReadLine();
                if (move == "")
                    break;
                chess = chess.Move(move);
            }

        }

        static string ChessToAscii(Chess chess)
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                    sb.Append(chess.GetPositionFigure(x, y) + " ");
                sb.AppendLine();
            }
            if (chess.IsCheckMate)
                Console.WriteLine("Checkmate");
            else if (chess.IsStalemate)
                Console.WriteLine("Stalemate");
            return sb.ToString();
        }
    }
}
