using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ft210007Lab7
{
    class Moves
    {
        FigureMoving fm;
        Board board;

        public Moves(Board board)
        {
            this.board = board;
        }

        public bool CanMove()
        {
            this.fm = fm;
            return
                CanMoveFrom() &&
                CanMoveTo() &&
                CanFigureMove();
        }

        bool CanMoveFrom()
        {
            return fm.from.OnBoard();
        }

        bool CanMoveTo()
        {
            return fm.to.OnBoard() &&
            fm.from != fm.to; //проверка прыжка на месте
        }

        bool CanFigureMove() //проверка правил хода фигуры
        {
            switch (fm.figure)
            {


                case Figure.Qeen:
                    return CanStraightMove();

                case Figure.Rook:
                    return (fm.SignX == 0 || fm.SignY == 0) &&
                        CanStraightMove();

                case Figure.Bishop:
                    return (fm.SignX != 0 || fm.SignY != 0) &&
                         CanStraightMove();

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
    }
}
