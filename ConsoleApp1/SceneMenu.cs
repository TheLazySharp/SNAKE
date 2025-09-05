using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace Code
{
    public class SceneMenu : Scene
    {
        public Methodes methodes = new Methodes();

        public static Button CommandsButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 380, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "Commands", 30, Color.Maroon, Color.LightGray);
        public static Button HighScoresButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 280, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "High Scores", 30, Color.Maroon, Color.LightGray);
        public static Button StartGameButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 180, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "New Game", 30, Color.Maroon, Color.LightGray);
        public static Button CloseGameButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 80, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "Quit Game", 30, Color.Maroon, Color.LightGray);
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
            Raylib.ClearBackground(new Color(10, 122, 33));

            methodes.DrawCenteredText("SNAKATOR", 50, 50, 4, Raylib.GetFontDefault(), new Color(153, 229, 80));
            Raylib.DrawTextureEx(TextureManager.snakeHeadLoadedB, new Vector2(Program.ScreenW*0.5f - 100, 120), 0, 10, Color.White);
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
                if (Program.nbGames == 0)
                {
                    StartGameButton.ButtonClic();
                    SceneManager.nextScene = SceneManager.enumScene.Game;
                    SceneManager.Load<SceneCommands>();
                }
                else
                {
                    StartGameButton.ButtonClic();
                    SceneManager.nextScene = SceneManager.enumScene.Game;
                    SceneManager.Load<SceneGame>();
                }
            }
        }

        public void CloseGameButtonEvent()
        {
            if (CloseGameButton.isVisible && CloseGameButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                CloseGameButton.ButtonClic();
                SceneManager.nextScene = SceneManager.enumScene.None;
                Program.closeGame = true;
            }
        }

        public void HighScoresButtonEvent()
        {
            if (HighScoresButton.isVisible && HighScoresButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                HighScoresButton.ButtonClic();
                SceneManager.nextScene = SceneManager.enumScene.HighScores;
                SceneManager.Load<SceneHighScores>();
            }
        }

        public void CommandsButtonEvent()
        {
            if (CommandsButton.isVisible && CommandsButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                CommandsButton.ButtonClic();
                SceneManager.nextScene = SceneManager.enumScene.Commands;
                SceneManager.Load<SceneCommands>();
            }
        }
    }
}
