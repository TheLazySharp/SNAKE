using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Color = Raylib_cs.Color;

namespace Code
{

    public class Grid
    {
        //public Grid() { }

        public readonly static int MAPH = 25;
        public readonly static int MAPW = 25;
        public readonly static int CELLH = 20;
        public readonly static int CELLW = 20;
        public readonly static int OFFSETX = 30;
        public readonly static int OFFSETY = 30;

        int[,] Map = new int[MAPH, MAPW];
        public void InitGrid()
        {
            for (int row = 0; row < MAPH; row++)
            {
                for (int column = 0; column < MAPW; column++)
                {
                    Map[row, column] = 0;
                }
            }
        }
    }
}
