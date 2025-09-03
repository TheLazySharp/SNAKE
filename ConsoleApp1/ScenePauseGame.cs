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
    public class ScenePauseGame : Scene
    {
        Controler.KeyboardDir saveDir = new Controler.KeyboardDir();
        public static int pauseMenuW = 800;
        public static int pauseMenuH = Program.ScreenH;

        public static Button ClosePauseButton = new Button((int)(pauseMenuW * 0.5f), pauseMenuH - 380, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "Back to Game", 30, Color.Maroon, Color.LightGray);
        public static Button CommandsButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 280, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "Commands", 30, Color.Maroon, Color.LightGray);
        public static Button MenuButton = new Button((int)(pauseMenuW * 0.5f), pauseMenuH - 180, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "Back to Menu", 30, Color.Maroon, Color.LightGray);
        public static Button CloseGameButton = new Button((int)(pauseMenuW * 0.5f), pauseMenuH - 80, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "Quit", 30, Color.Maroon, Color.LightGray);
        public ScenePauseGame()
        {
        }
        public override void Load()
        {
            SceneManager.runningScene = SceneManager.enumScene.Pause;
            ClosePauseButton.isVisible = true;
            CommandsButton.isVisible = true;
            MenuButton.isVisible = true;
            CloseGameButton.isVisible = true;

            if (SceneGame.GameOnPause)
            {
                saveDir = Controler.dir;
                Controler.nextDir = Controler.KeyboardDir.Freeze;
            }
        }

        public override void Update(float deltatime)
        {
            if (SceneGame.GameOnPause)
            {
                ClosePauseButton.MouseHover(ClosePauseButton);
                MenuButton.MouseHover(MenuButton);
                CommandsButton.MouseHover(CommandsButton);
                CloseGameButton.MouseHover(CloseGameButton);
                ClosePauseButtonEvent();
                MenuButtonEvent();
                CloseGameButtonEvent();
                CommandsButtonEvent();

            }

        }

        public override void Draw()
        {
            Raylib.DrawRectangle(Program.ScreenW / 2 - pauseMenuW / 2, 0, pauseMenuW, pauseMenuH, Color.LightGray);
            Raylib.DrawText("PAUSE", (int)(pauseMenuW * 0.5f), 50, 50, Color.Maroon);

            ClosePauseButton.ButtonDraw();
            MenuButton.ButtonDraw();
            CloseGameButton.ButtonDraw();
            CommandsButton.ButtonDraw();
        }

        public override void Unload()
        {
            ClosePauseButton.isVisible = false;
            MenuButton.isVisible = false;
            CommandsButton.isVisible = false;
            CloseGameButton.isVisible = false;
            SceneManager.previousScene = SceneManager.runningScene;

        }

        public void ClosePauseButtonEvent()
        {
            if (ClosePauseButton.isVisible && ClosePauseButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                SceneManager.nextScene = SceneManager.enumScene.Game;

                Controler.nextDir = saveDir;
                SceneGame.GameOnPause = false;
                SceneManager.Load<SceneGame>();

                Console.WriteLine("Pause OFF");
            }
        }

        public void CloseGameButtonEvent()
        {
            if (CloseGameButton.isVisible && CloseGameButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                SceneManager.nextScene = SceneManager.enumScene.None;
                Program.closeGame = true;
            }
        }

        public void MenuButtonEvent()
        {
            if (MenuButton.isVisible && MenuButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                SceneManager.nextScene = SceneManager.enumScene.Menu;
                SceneManager.Load<SceneGame>();
            }
        }
        public void CommandsButtonEvent()
        {
            if (CommandsButton.isVisible && CommandsButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                SceneManager.nextScene = SceneManager.enumScene.Commands;
                SceneManager.Load<SceneCommands>();
            }
        }
    }
}
