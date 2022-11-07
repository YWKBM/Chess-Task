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
            Board board = new Board();
            Moves moves = new Moves(board);

            Console.WriteLine("Enter the x pozition of figure: ");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the y pozition of figure: ");
            int y = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the name of figure (Q,B,R,N): ");
            string inputName = Console.ReadLine();

            Figure figure = (Figure)inputName[0];

            if (((char)figure != (char)Figure.Qeen && (char)figure != (char)Figure.Knight && (char)figure != (char)Figure.Rook && (char)figure != (char)Figure.Bishop) || (char)figure == (char)Figure.Target)
            {
                Console.WriteLine("Wrong figure");
            }

            Console.WriteLine("Enter the x pozition of target: ");
            int xT = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the y pozition of target: ");
            int yT = int.Parse(Console.ReadLine());

            board.SetFigureAt(x, y, figure);
            board.SetFigureAt(xT, yT, Figure.Target);

            Console.WriteLine(BoardToAcsii(board));

            Square[] allowedSquare = moves.FindAllMoves(board.fs[x, y]);

            foreach (var sq in allowedSquare)
            {
                Console.WriteLine(sq.x + ";" + sq.y);
            }

            bool canMove = moves.CanMove(new FigureMoving(board.GetFigureAt(x, y), board.fs[x, y].square, board.fs[xT, yT].square));
            Console.WriteLine("Turn to reach the target: " + canMove);
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
