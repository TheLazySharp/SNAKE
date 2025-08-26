using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class Methodes
    {
        public Vector2 ToWorld(int column, int row)
        {
            int x = column * Grid.CELLW + Grid.OFFSETX;
            int y = row * Grid.CELLH + Grid.OFFSETY;
            return new Vector2(x, y);
        }

        public (int, int) ToGrid(Vector2 position)
        {
            int column = (int)position.X / (Grid.CELLW + Grid.OFFSETX);
            int row = (int)position.Y / (Grid.CELLH + Grid.OFFSETY);
            return (column, row);
        }

        public (int, int) CastVector(Vector2 position)
        {
            int x = (int)position.X;
            int y = (int)position.Y;
            return (x, y);
        }

        public (int, int) GridToWorld(int column, int row)
        {
            int x = column * Grid.CELLW + Grid.OFFSETX;
            int y = row * Grid.CELLH + Grid.OFFSETY;
            return (x, y);
        }

        public bool IsWallOnSnake(Wall wall)
        {
            if (wall.column <= Game.snakeHeadInit + 1
                && wall.column >= Game.snakeHeadInit - (Wall.maxSize +3)
                && wall.row <= Game.snakeHeadInit + 1
                && wall.row >= Game.snakeHeadInit - Wall.maxSize)
            {
                return true;
            }
            else return false;
        }

        //public bool GetCell(int column, int row)
        //{
        //    if (column < 0) return false;
        //    if (row < 0) return false;
        //    if (column >= Grid.MAPW) return false;
        //    if (row >= Grid.MAPH) return false;

        //    return cells[column, row];
        //}

        //public bool[] MooreNeighborhood(int column, int row)
        //{
        //    bool[] neighbors = new bool[8]; 
        //    neighbors
        //}
    }
}
