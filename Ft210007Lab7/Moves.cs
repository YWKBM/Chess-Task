using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ft210007Lab7
{
    class Moves
    {
        Board board;

        public Moves(Board board)
        {
            this.board = board;
        }

        public bool CanMove(FigureMoving fm)
        {
            return
                CanMoveFrom(fm) &&
                CanMoveTo(fm) &&
                CanFigureMove(fm);
        }

        bool CanMoveFrom(FigureMoving fm)
        {
            return fm.from.OnBoard();
        }

        bool CanMoveTo(FigureMoving fm)
        {
            return fm.to.OnBoard() &&
            fm.from != fm.to; //проверка прыжка на месте
        }

        bool CanFigureMove(FigureMoving fm) //проверка правил хода фигуры
        {
            switch (fm.figure)
            {
                case Figure.Qeen:
                    return (fm.DeltaX == fm.DeltaY || fm.DeltaX == 0 || fm.DeltaY == 0);

                case Figure.Rook:
                    return (fm.SignX == 0 || fm.SignY == 0) &&
                        CanStraightMove();

                case Figure.Bishop:
                    return fm.AbsDeltaX == fm.AbsDeltaY;
                //CanStraightMove() && (fm.SignX != 0 || fm.SignY != 0) ???

                case Figure.Knight:
                    return CanKnightMove();


                default: return false;
            }

            bool CanKnightMove()
            {
                if (fm.AbsDeltaX == 1 && fm.AbsDeltaY == 2) return true;
                if (fm.AbsDeltaX == 2 && fm.AbsDeltaY == 1) return true;
                return false;
            }

        bool CanStraightMove()
        {
            Square at = (fm.from);
            do
            {
                at = new Square(at.x + fm.SignX, at.y + fm.SignY);
                if (at == fm.to)
                    return true;
            } while (at.OnBoard() && 
            board.GetFigureAt(at.x, at.y) == Figure.none);
            return false;
        }



        }

        public List<Square> FindAllMoves(FigureOnSquare check)
        {
            List<Square> allowedSquares = new List<Square>();
            for (int x = 0; x < 8; x++)
            {
                for(int y = 0; y < 8; y++)
                {
                    //bool canMove = moves.CanMove(new FigureMoving(board.GetFigureAt(x, y), board.fs[x, y].square, board.fs[xT, yT].square));
                    if (CanMove(new FigureMoving(check.figure, board.fs[check.square.x, check.square.y].square, board.fs[x, y].square)))
                    {
                        allowedSquares.Add(board.fs[x, y].square);
                    }
                }
            }
            return allowedSquares;
        }

    }
}
