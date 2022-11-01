using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ft210007Lab7
{
    internal class Board
    {
        public FigureOnSquare[,] fs { get; set; }  

        public Board()
        {
            fs = new FigureOnSquare[8, 8];
            Init();
        }

        public void Init()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y =0; y < 8; y++)
                {
                    this.fs[x,y]= new FigureOnSquare(new Square(x, y), Figure.none);
                }
            }
        }

        public void SetFigureAt(int x, int y, Figure fg)
        {
            fs[x, y] = new FigureOnSquare(new Square(x,y),fg);  
        }

        public Figure GetFigureAt(int x, int y)
        {
                return fs[x, y].figure;
        }

        bool ContaintsFigureAt(int x, int y)
        {
            return fs[x, y] != null;    
        }
    }
}
