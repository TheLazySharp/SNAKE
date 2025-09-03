using Code;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Color = Raylib_cs.Color;
using System.Diagnostics.Metrics;

namespace Code
{

    public class Drawer
    {
        public Methodes methodes = new Methodes();
        public ScenePauseGame pause = new ScenePauseGame();
        public ScoreManager scoreManager = new ScoreManager();
        private static int PANELW = Program.ScreenW - (Grid.MAPW * Grid.CELLW + 3 * Grid.OFFSETY);
        private static int PANELH = Grid.MAPH * Grid.CELLH;
        private static int PANELX = Grid.MAPW * Grid.CELLW + 2 * Grid.OFFSETX;
        private static int PANELY = Grid.OFFSETY;
        private static int PANELSPACER = 40;

        public void Draw()
        {
            GridDraw();
            WallsDraw();
            SnakeDraw();
            LootsDraw();
            EdgesDraw();
            PanelDraw();
            ScoreManager.DrawPopingScores();

        }
        public void SnakeDraw()
        {
            for (int i = 0; i < Snake.ListBodySnake.Count; i++)
            {
                Snake snake = Snake.ListBodySnake[i];
                (int x, int y) SnakePart = methodes.GridToWorld(snake.Column, snake.Row);
                if (snake.Type == "head")
                {
                    Raylib.DrawRectangle(SnakePart.x, SnakePart.y, Grid.CELLW, Grid.CELLH, snake.Color);
                }
                else if (snake.Type == "body")
                {
                    Raylib.DrawRectangle(SnakePart.x, SnakePart.y, Grid.CELLW, Grid.CELLH, new Color(10, 122, 33, 255 * (Snake.ListBodySnake.Count - i) / Snake.ListBodySnake.Count));
                }
            }
        }

        public void GridDraw()
        {
            for (int row = 0; row < Grid.MAPH; row++)
            {
                for (int column = 0; column < Grid.MAPW; column++)
                {
                    int y = row * Grid.CELLH;
                    int x = column * Grid.CELLW;
                    Raylib.DrawRectangleLines(x + Grid.OFFSETX, y + Grid.OFFSETY, Grid.CELLW, Grid.CELLH, Color.White);
                }
            }
        }
        public void LootsDraw()
        {
            for (int i = 0; i < Loot.ListLoots.Count; i++)
            {
                Loot loot = Loot.ListLoots[i];
                (int x, int y) loots = methodes.GridToWorld(loot.column, loot.row);
                if (loot.isVisible)
                {
                    if (loot.type == "apple")
                    {
                        Raylib.DrawCircle((int)(loots.x + Grid.CELLW * 0.5f), (int)(loots.y + Grid.CELLH * 0.5f), Grid.CELLH * 0.5f, loot.color);
                    }
                    if (loot.type == "hedgehog")
                    {
                        Vector2 vTop = new Vector2(loots.x + Grid.CELLW * 0.5f, loots.y);
                        Vector2 vLeft = new Vector2(loots.x, loots.y + Grid.CELLH);
                        Vector2 vRight = new Vector2(loots.x + Grid.CELLW, loots.y + Grid.CELLH);

                        Raylib.DrawTriangle(vTop, vLeft, vRight, loot.color);
                    }

                    if (loot.type == "bullet")
                    {
                        Raylib.DrawRectangle(loots.x, loots.y, Grid.CELLW, Grid.CELLH, loot.color);
                    }
                }

            }
        }

        public void WallsDraw()
        {
            for (int i = 0; i < Wall.ListWall.Count; i++)
            {
                Wall wall = Wall.ListWall[i];
                (int x, int y) wallpart = methodes.GridToWorld(wall.column, wall.row);
                Raylib.DrawRectangle(wallpart.x, wallpart.y, Grid.CELLW, Grid.CELLH, wall.color);

            }
        }

        public void EdgesDraw()
        {
            Raylib.DrawRectangleLines(Grid.OFFSETX, Grid.OFFSETY, Grid.CELLW * Grid.MAPW, Grid.CELLH * Grid.MAPH, Color.Maroon);
        }

        public void PanelDraw()
        {
            Raylib.DrawRectangleLines(PANELX, PANELY, PANELW, PANELH, Color.Maroon);
            Raylib.DrawText("SNAKATOR", PANELX + 5, PANELY + 10, 40, Color.Maroon);
            Raylib.DrawText($"SCORE : {ScoreManager.score}", PANELX + 5, PANELY + 2 * PANELSPACER, 30, Color.Maroon);
            Raylib.DrawText(($"x {Math.Round((ScoreManager.scoreMultiplier), 2)}"), PANELX + 5, PANELY + 3 * PANELSPACER, 25, Color.Maroon);
            Raylib.DrawText($"Walls : {Wall.ListWall.Count}/{Game.nbWallPart}", PANELX + 5, PANELY + 4 * PANELSPACER, 30, Color.Maroon);
            Raylib.DrawText($"Snake size : {Snake.ListBodySnake.Count}", PANELX + 5, PANELY + 5 * PANELSPACER, 30, Color.Maroon);
            Raylib.DrawText($"Max snake size : {Game.maxSnakeSize}", PANELX + 5, PANELY + 6 * PANELSPACER, 30, Color.Maroon);
            Raylib.DrawText($"Apple eaten : {ScoreManager.nbAppleEaten}", PANELX + 5, PANELY + 7 * PANELSPACER, 30, Color.Maroon);
            Raylib.DrawText($"Hedgehog killed :{Game.nbHedgeHogKilled}", PANELX + 5, PANELY + 8 * PANELSPACER, 30, Color.Maroon);
            Raylib.DrawText($"Bullet shots :", PANELX + 5, PANELY + 9 * PANELSPACER, 30, Color.Maroon);
        }
    }
}
