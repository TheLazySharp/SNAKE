using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace Code
{
    public class SceneMenu : Scene
    {
        public Methodes methodes = new Methodes();

        public static Button CommandsButton = new Button((int)(Program.ScreenW * 0.5f-110), Program.ScreenH - 380, 220, 50, Color.White, Color.Black, Color.LightGray, "Commands", 30, Color.White, Color.Black);
        public static Button HighScoresButton = new Button((int)(Program.ScreenW * 0.5f-110), Program.ScreenH - 280, 220, 50, Color.White, Color.Black, Color.LightGray, "High Scores", 30, Color.White, Color.Black);
        public static Button StartGameButton = new Button((int)(Program.ScreenW * 0.5f-110), Program.ScreenH - 180, 220, 50, Color.White, Color.Black, Color.LightGray, "New Game", 30, Color.White, Color.Black);
        public static Button CloseGameButton = new Button((int)(Program.ScreenW * 0.5f-110), Program.ScreenH - 80, 220, 50, Color.White, Color.Black, Color.LightGray, "Quit Game", 30, Color.White, Color.Black);
        public override void Load()
        {
            SceneManager.runningScene = SceneManager.enumScene.Menu;
            CommandsButton.isVisible = true;
            HighScoresButton.isVisible = true;
            StartGameButton.isVisible = true;
            CloseGameButton.isVisible = true;
        }

        public override void Update(float deltatime)
        {
            StartGameButton.MouseHover(StartGameButton);
            CommandsButton.MouseHover(CommandsButton);
            HighScoresButton.MouseHover(HighScoresButton);
            CloseGameButton.MouseHover(CloseGameButton);
            StartGameButtonEvent();
            CommandsButtonEvent();
            CloseGameButtonEvent();
            HighScoresButtonEvent();
        }
        public override void Draw()
        {
            Raylib.ClearBackground(Color.Black);

            methodes.DrawCenteredText("SNAKE SURVIVOR", 100, 50, 4, Raylib.GetFontDefault(), Color.White);
            CommandsButton.ButtonDraw();
            HighScoresButton.ButtonDraw();
            StartGameButton.ButtonDraw();
            CloseGameButton.ButtonDraw();

            //Console.WriteLine("Drawing Menu scene");
        }

        public override void Unload()
        {
            CommandsButton.isVisible = false;
            StartGameButton.isVisible = false;
            HighScoresButton.isVisible = false;
            CloseGameButton.isVisible = false;
            SceneManager.previousScene = SceneManager.runningScene;

        }

        public void StartGameButtonEvent()
        {
            if (StartGameButton.isVisible && StartGameButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                SceneManager.nextScene = SceneManager.enumScene.Game;
                SceneManager.Load<SceneGame>();
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

        public void HighScoresButtonEvent()
        {
            if (HighScoresButton.isVisible && HighScoresButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                SceneManager.nextScene = SceneManager.enumScene.HighScores;
                SceneManager.Load<SceneHighScores>();
            }
        }

        public void CommandsButtonEvent()
        {
            if (HighScoresButton.isVisible && HighScoresButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                SceneManager.nextScene = SceneManager.enumScene.Commands;
                SceneManager.Load<SceneCommands>();
            }
        }
    }
}
