using Raylib_cs;
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
                && wall.column >= Game.snakeHeadInit - (Wall.maxSize + 3)
                && wall.row <= Game.snakeHeadInit + 1
                && wall.row >= Game.snakeHeadInit - Wall.maxSize)
            {
                return true;
            }
            else return false;
        }


        public void DrawCenteredText(string text, int y, int fontSize, int fontSpacing, Font font, Color color)
        {
            Vector2 textSize = Raylib.MeasureTextEx(font, text, fontSize, fontSpacing);
            Vector2 textPos;
            int w = Program.ScreenW;
            textPos.X = w * 0.5f - textSize.X * 0.5f;
            textPos.Y = y;

            Raylib.DrawTextEx(font, text, textPos, fontSize, fontSpacing, color);
        }
    }
}
