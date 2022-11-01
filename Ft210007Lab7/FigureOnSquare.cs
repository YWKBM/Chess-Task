using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ft210007Lab7
{
    class FigureOnSquare
    {
        public Square square { get; set; }
        public Figure figure { get; set; }

        public FigureOnSquare(Square square, Figure figure)
        {
            this.square = square;
            this.figure = figure;
        }   


    }
}
