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
    public class PauseGame
    {
        Controler.KeyboardDir saveDir = new Controler.KeyboardDir();
        public static int pauseMenuW = 800;
        public static int pauseMenuH = Program.ScreenH;
        int MouseX;
        int MouseY;

        public static Button ClosePauseButton = new Button((int)(pauseMenuW * 0.5f), pauseMenuH - 180, 220, 50, Color.White, Color.Black, Color.LightGray, "Back to Game", 30, Color.White, Color.Black);
        public static Button CloseGameButton = new Button((int)(pauseMenuW * 0.5f), pauseMenuH - 80, 220, 50, Color.White, Color.Black, Color.LightGray, "Quit Game", 30, Color.White, Color.Black);
        public PauseGame()
        {
        }
        public void LoadPause()
        {
            if (GameScene.GameOnPause)
            {
                saveDir = Controler.nextDir;
                Controler.nextDir = Controler.KeyboardDir.Freeze;
            }
        }

        public void UpdatePause()
        {
            if (GameScene.GameOnPause)
            {
                ClosePauseButton.isVisible = true;
                CloseGameButton.isVisible = true;
                ClosePauseButton.MouseHover(ClosePauseButton);
                CloseGameButton.MouseHover(ClosePauseButton);
                ClosePauseButtonEvent();
                CloseGameButtonEvent();
            }

        }

        public void PauseDraw()
        {
            Raylib.DrawRectangle(Program.ScreenW / 2 - pauseMenuW / 2, 0, pauseMenuW, pauseMenuH, Color.Black);
            Raylib.DrawText("PAUSE", (int)(pauseMenuW * 0.5f), 50, 50, Color.White);

            ClosePauseButton.ButtonDraw();
            CloseGameButton.ButtonDraw();
        }

        public void ClosePauseButtonEvent()
        {
            if (ClosePauseButton.isVisible && ClosePauseButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Controler.nextDir = saveDir;
                GameScene.GameOnPause = false;
                Console.WriteLine("Pause OFF");
            }
        }

        public void CloseGameButtonEvent()
        {
            if (CloseGameButton.isVisible && CloseGameButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Program.closeGame = true;
            }
        }
    }
}
