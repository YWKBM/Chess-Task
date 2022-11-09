using System;
using System.Collections.Generic;
using System.IO;
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
            StreamWriter logger = new StreamWriter("Logger.txt");

            while (true){
                Board board = new Board();
                logger.WriteLine("Board initialized");
                Moves moves = new Moves(board);
                logger.WriteLine("Moves rules initialized");

                //инициализация координаты фигуры
                Console.WriteLine("Enter the x pozition of figure [0 : 7]: ");
                int x = int.Parse(Console.ReadLine());
                logger.WriteLine("User added coordinate x: " + x);
                Console.WriteLine("Enter the y pozition of figure [0 : 7]: ");
                int y = int.Parse(Console.ReadLine());
                logger.WriteLine("User added coordinate y: " + y);

                if (x < 0 || y < 0 || x >= 8 || y >=8)//обработка ошибок ввода
                { 
                    Console.WriteLine("Board doesn't include this coordinates, try again");
                    logger.WriteLine("User entered wrong coordinates: " + x + ";" + y); 
                    break;
                }

            //инициализаци фигуры
                Console.WriteLine("Enter the name of figure (Q,B,R,N): ");
                string inputName = Console.ReadLine();

                Figure figure = (Figure)inputName[0];

                if (((char)figure != (char)Figure.Qeen && (char)figure != (char)Figure.Knight && (char)figure != (char)Figure.Rook && (char)figure != (char)Figure.Bishop) || (char)figure == (char)Figure.Target || (char)figure == (char)Figure.Pozition)
                {
                    Console.WriteLine("Wrong figure, try again");
                    logger.WriteLine("User entered wrong figure name: " + (char)figure);
                    break;
                }
                

            //инициализация цели
                Console.WriteLine("Enter the x pozition of target: ");
                int xT = int.Parse(Console.ReadLine());
                logger.WriteLine("User added coordinate x: " + xT);
                Console.WriteLine("Enter the y pozition of target: ");
                int yT = int.Parse(Console.ReadLine());
                logger.WriteLine("User added coordinate y: " + xT);
                if (xT < 0 || yT < 0 || xT >= 8 || yT >=8) //обработка ошибок ввода
                { 
                    Console.WriteLine("Board doesn't include this coordinates, try again");
                    logger.WriteLine("User entered wrong coordinates: " + xT + ";" + yT);
                    break;
                }
                if (SquaresSameColor(board.fs[x, y].square, board.fs[xT, yT].square))
                {
                    Console.WriteLine("The color of target's and figure's pozition is the same");

                }
                else 
                {
                    Console.WriteLine("The color of target's and figure's pozition is different");
                }


                board.SetFigureAt(x, y, figure);
                logger.WriteLine("Figure " + (char)figure + " setted on " + x + ";" + y);
                board.SetFigureAt(xT, yT, Figure.Target);
                logger.WriteLine("Figure " + (char)Figure.Target + " setted on " + xT + ";" + yT);



                //вывод доски
                Console.WriteLine(BoardToAcsii(board));

                //проверка возможности достижения цели за один ход
                bool canMove = moves.CanMove(new FigureMoving(board.GetFigureAt(x, y), board.fs[x, y].square, board.fs[xT, yT].square));
                Console.WriteLine("Turn to reach the target: " + canMove);
                if (!canMove)
                {
                    //формирование массива доступных ходов 
                    List<Square> allowedSquare = moves.FindAllMoves(board.fs[x, y]);
                    logger.WriteLine("Array of figures moves formation");
                    foreach (Square square in allowedSquare)
                    {
                    logger.WriteLine(square.x + ";" + square.y);   
                    }


                    //формирования массива доступных ходов заданной фигуры с позиции цели
                                       
                    board.SetFigureAt(xT, yT, figure);
                    logger.WriteLine("Figure " + (char)figure + " setted on " + xT + ";" + yT);
                    logger.WriteLine("Array of figures moves formation");
                    List<Square> AllowedMovesFromTarget = moves.FindAllMoves(board.fs[xT, yT]);
                    foreach(Square allowedMove in AllowedMovesFromTarget)
                    {
                        logger.WriteLine(allowedMove.x + ";" + allowedMove.y);  
                    }
                    board.SetFigureAt(xT, yT, Figure.Target);
                    logger.WriteLine("Figure " + (char)Figure.Target + " setted on " + xT + ";" + yT);



                    //Поиск пересечений
                    logger.WriteLine("Searching for intersections");
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
                    {
                        logger.WriteLine("Intersection has been found: " + secondMove.x + ";" + secondMove.y);
                        Console.WriteLine("Target can be reached by second move from X pozition with coordinates: " + secondMove.x + ";" + secondMove.y);
                        board.SetFigureAt(secondMove.x, secondMove.y, Figure.Pozition);
                        logger.WriteLine("Figure " + (char)Figure.Pozition + " setted on " + secondMove.x + ";" + secondMove.y);
                        Console.WriteLine(BoardToAcsii(board));
                    }


                    else
                    {
                        Console.WriteLine("Target can't be reached by second move");
                        logger.WriteLine("Intersection hasn't been found");
                    }
                }
                else
                {
                    logger.WriteLine("Target can be reached by first move");
                }
                break;
            }
        
            logger.Close();
            
            

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
        public static bool SquaresSameColor(Square f, Square t)
        {
            return ((f.x + f.y) % 2 == 0) && ((t.x + t.y) % 2 == 0);
        }

        public static char GetFigureFrom(int x, int y, Board board)
        {
            Square square = new Square(x, y);
            Figure f = board.GetFigureAt(x, y);
            return f == Figure.none ? '.' : (char)f;
        }
    }
}
