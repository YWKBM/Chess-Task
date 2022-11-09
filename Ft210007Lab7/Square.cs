using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ft210007Lab7
{
    struct Square
    {
        public static Square none = new Square(-1, -1);
        public int x { get; set; }
        public int y { get; set; }
        

        public Square(int x, int y)
        {
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                this.x = x;
                this.y = y;
            }
            else
            {
                this = none;
            }
        }

        public bool OnBoard()
        {
            return this.x >= 0 && this.x < 8 && this.y >= 0 && this.y < 8; 
        }

        //перегрузка оператора "==" для сравнения Square
        public static bool operator ==(Square a, Square b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(Square a, Square b)
        {
            return a.x != b.x || a.y != b.y;
        }

    }
}
