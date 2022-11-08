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
            while (true){ 
            Board board = new Board();
            Moves moves = new Moves(board);

                //инициализация координаты фигуры
            Console.WriteLine("Enter the x pozition of figure: ");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the y pozition of figure: ");
            int y = int.Parse(Console.ReadLine());

            if(x < 0 || y < 0 || x >= 8 || y >=8)//обработка ошибок ввода
            { 
                    Console.WriteLine("Board doesn't include this coordinates, try again");
                    break;
            }

            //инициализаци фигуры
            Console.WriteLine("Enter the name of figure (Q,B,R,N): ");
            string inputName = Console.ReadLine();

            Figure figure = (Figure)inputName[0];

            if (((char)figure != (char)Figure.Qeen && (char)figure != (char)Figure.Knight && (char)figure != (char)Figure.Rook && (char)figure != (char)Figure.Bishop) || (char)figure == (char)Figure.Target || (char)figure == (char)Figure.Pozition)
            {
                Console.WriteLine("Wrong figure, try again");
                    break;
            }

            //инициализация цели
            Console.WriteLine("Enter the x pozition of target: ");
            int xT = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the y pozition of target: ");
            int yT = int.Parse(Console.ReadLine());
            if(xT < 0 || yT < 0 || xT >= 8 || yT >=8) //обработка ошибок ввода
            { 
                    Console.WriteLine("Board doesn't include this coordinates, try again");
                    break;
            }

            board.SetFigureAt(x, y, figure);
            board.SetFigureAt(xT, yT, Figure.Target);
                   
       
            //вывод доски
            Console.WriteLine(BoardToAcsii(board));

            //формирование массива доступных ходов 
            List<Square> allowedSquare = moves.FindAllMoves(board.fs[x, y]);

            
            //проверка возможности достижения цели за один ход
            bool canMove = moves.CanMove(new FigureMoving(board.GetFigureAt(x, y), board.fs[x, y].square, board.fs[xT, yT].square));
            Console.WriteLine("Turn to reach the target: " + canMove);

            //формирования массива доступных ходов заданной фигуры с позиции цели
            if (!canMove) { 
            board.SetFigureAt(xT, yT, figure);
            List<Square> AllowedMovesFromTarget = moves.FindAllMoves(board.fs[xT, yT]);
            board.SetFigureAt(xT, yT, Figure.Target);

            //Поиск пересечений

            Square secondMove = Square.none;
            for (int i = 0; i < AllowedMovesFromTarget.Count; i++)
            {
                if (allowedSquare.Contains(AllowedMovesFromTarget[i]))
                {
                    secondMove = AllowedMovesFromTarget[i];
                    break;
                }
            }
            Console.WriteLine();
           
            if (secondMove != Square.none)
                 Console.WriteLine("Target can be reached by second move from X pozition with coordinates: " + secondMove.x + ";" + secondMove.y);
            else
            {
                Console.WriteLine("Target can't be reached by second move");
            }

            board.SetFigureAt(secondMove.x, secondMove.y, Figure.Pozition);
            Console.WriteLine(BoardToAcsii(board));
            }
            }
            
            

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
