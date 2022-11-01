using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ft210007Lab7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();
            Moves moves = new Moves(board);
            board.SetFigureAt(1, 1, Figure.Qeen);
            board.SetFigureAt(7, 7, Figure.Target);
            Console.WriteLine(BoardToAcsii(board));
        }

        public static string BoardToAcsii(Board board)
        {
            string text = "  +-----------------+\n";
            for (int y = 7; y >= 0; y--)
            {
                text += y + 1;
                text += " | ";
                for (int x = 0; x < 8; x++)
                {
                    text += GetFigureFrom(x, y, board) + " ";
                }
                text += "|\n";
            }
            text += "  +-----------------+\n";
            text += "    a b c d e f g h\n";
            return text;
        }

        public static char GetFigureFrom(int x, int y, Board board)
        {
            Square square = new Square(x, y);
            Figure f = board.GetFigureAt(x, y);
            return f == Figure.none ? '.' : (char)f;
        }
    }
}
