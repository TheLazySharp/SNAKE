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
        private static int PANELW = Program.ScreenW - (Grid.MAPW * Grid.CELLW + 3 * Grid.OFFSETX);
        private static int PANELH = Grid.MAPH * Grid.CELLH;
        private static int PANELX = Grid.MAPW * Grid.CELLW + 2 * Grid.OFFSETX;
        private static int PANELY = Grid.OFFSETY;
        private static int PANELSPACER = 40;
        private Texture2D currentTailTexture;
        Color textColor = Program.greenLemon;
        Color backgroundColor = Program.darkGreen;

        public void Draw()
        {
            Raylib.ClearBackground(backgroundColor);
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
                    switch (Controler.nextDir)
                    {
                        case Controler.KeyboardDir.Start:
                            Raylib.DrawTexture(TextureManager.snakeHeadR, SnakePart.x, SnakePart.y, Color.White);
                            break;

                        case Controler.KeyboardDir.Right:
                            if (Snake.Ammo == 0) Raylib.DrawTexture(TextureManager.snakeHeadR, SnakePart.x, SnakePart.y, Color.White);
                            else Raylib.DrawTexture(TextureManager.snakeHeadLoadedR, SnakePart.x, SnakePart.y, Color.White);
                            break;

                        case Controler.KeyboardDir.Down:
                            if (Snake.Ammo == 0) Raylib.DrawTexture(TextureManager.snakeHeadB, SnakePart.x, SnakePart.y, Color.White);
                            else Raylib.DrawTexture(TextureManager.snakeHeadLoadedB, SnakePart.x, SnakePart.y, Color.White);
                            break;

                        case Controler.KeyboardDir.Left:
                            if (Snake.Ammo == 0) Raylib.DrawTexture(TextureManager.snakeHeadL, SnakePart.x, SnakePart.y, Color.White);
                            else Raylib.DrawTexture(TextureManager.snakeHeadLoadedL, SnakePart.x, SnakePart.y, Color.White);
                            break;

                        case Controler.KeyboardDir.Up:
                            if (Snake.Ammo == 0) Raylib.DrawTexture(TextureManager.snakeHeadU, SnakePart.x, SnakePart.y, Color.White);
                            else Raylib.DrawTexture(TextureManager.snakeHeadLoadedU, SnakePart.x, SnakePart.y, Color.White);
                            break;
                    }

                }
                
                else if (snake.Type == "body")
                {
                    if (i == Snake.ListBodySnake.Count - 1)
                    {
                        if (Controler.nextDir == Controler.KeyboardDir.Start)
                        {
                            Raylib.DrawTexture(TextureManager.snakeTailR, SnakePart.x, SnakePart.y, Color.White);
                            currentTailTexture = TextureManager.snakeTailR;
                        }

                        Coordinates TailPos = new Coordinates(Snake.ListBodySnake[i-1].Column, Snake.ListBodySnake[i-1].Row);

                        if (Snake.HeadDir.Count > 0 && TailPos == Snake.HeadDir.First().Item1)
                        {
                            switch (Snake.HeadDir.First().Item2)
                            {

                                case Controler.KeyboardDir.Right:
                                    Raylib.DrawTexture(TextureManager.snakeTailR, SnakePart.x, SnakePart.y, Color.White);
                                    currentTailTexture = TextureManager.snakeTailR;
                                    Snake.HeadDir.Dequeue();
                                    break;

                                case Controler.KeyboardDir.Down:
                                    Raylib.DrawTexture(TextureManager.snakeTailB, SnakePart.x, SnakePart.y, Color.White);
                                    currentTailTexture = TextureManager.snakeTailB;
                                    Snake.HeadDir.Dequeue();
                                    break;

                                case Controler.KeyboardDir.Left:
                                    Raylib.DrawTexture(TextureManager.snakeTailL, SnakePart.x, SnakePart.y, Color.White);
                                    currentTailTexture = TextureManager.snakeTailL;
                                    Snake.HeadDir.Dequeue();
                                    break;

                                case Controler.KeyboardDir.Up:
                                    Raylib.DrawTexture(TextureManager.snakeTailU, SnakePart.x, SnakePart.y, Color.White);
                                    currentTailTexture = TextureManager.snakeTailU;
                                    Snake.HeadDir.Dequeue();
                                    break;
                            }
                        }

                        else Raylib.DrawTexture(currentTailTexture, SnakePart.x, SnakePart.y, Color.White);

                    }
                    else Raylib.DrawRectangle(SnakePart.x, SnakePart.y, Grid.CELLW, Grid.CELLH, new Color(10, 122, 33, 255 * (Snake.ListBodySnake.Count - i) / Snake.ListBodySnake.Count));
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
                    Raylib.DrawRectangle(x + Grid.OFFSETX, y + Grid.OFFSETY, Grid.CELLW, Grid.CELLH, Color.LightGray);
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
                        Raylib.DrawTexture(TextureManager.apple, loots.x, loots.y, Color.White);
                    }
                    if (loot.type == "hedgehog")
                    {
                        Raylib.DrawTexture(TextureManager.hedgehog, loots.x, loots.y, Color.White);
                    }

                    if (loot.type == "bullet")
                    {
                        if (Game.bulletIsShot) Raylib.DrawTexture(TextureManager.venom, loots.x, loots.y, Color.White);
                        else Raylib.DrawTexture(TextureManager.poison, loots.x, loots.y, Color.White);
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
                Raylib.DrawTexture(TextureManager.wall, wallpart.x, wallpart.y, Color.White);
            }
        }

        public void EdgesDraw()
        {
            Raylib.DrawRectangleLines(Grid.OFFSETX, Grid.OFFSETY, Grid.CELLW * Grid.MAPW, Grid.CELLH * Grid.MAPH, textColor);
        }

        public void PanelDraw()
        {
            methodes.DrawCenteredText("SNAKATOR", 10, 50, 4, Raylib.GetFontDefault(), textColor);

            Raylib.DrawText("PLAYER SCORE", Program.ScreenW/2 + 250, 15, 30, textColor);
            Raylib.DrawText($"{ScoreManager.score}", Program.ScreenW / 2 + 350, 50, 30, Color.Red);
            
            Raylib.DrawText("HIGH SCORE", 30, 15, 30, textColor);
            Raylib.DrawText($"{ScoreManager.ScoreTobeat}", 30 + 50, 50, 30, textColor);

            //Raylib.DrawRectangleLines(PANELX, PANELY, PANELW, PANELH, textColor);
            Raylib.DrawText(($"Bonus: x {Math.Round((ScoreManager.scoreMultiplier), 2)}"), PANELX + 5, PANELY + 0 * PANELSPACER, 25, textColor);
            Raylib.DrawText($"Snake size : {Snake.ListBodySnake.Count}", PANELX + 5, PANELY + 1 * PANELSPACER, 30, textColor);
            Raylib.DrawText($"Max snake size : {Game.maxSnakeSize}", PANELX + 5, PANELY + 2 * PANELSPACER, 30, textColor);
            Raylib.DrawText($"Apple eaten : {ScoreManager.nbAppleEaten}", PANELX + 5, PANELY + 3 * PANELSPACER, 30, textColor);
            Raylib.DrawText($"Hedgehog killed :{Game.nbHedgeHogKilled}", PANELX + 5, PANELY + 4 * PANELSPACER, 30, textColor);
        }
    }
}
