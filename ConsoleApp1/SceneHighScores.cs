using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace Code
{
    public class SceneHighScores : Scene
    {
        public Methodes methodes = new Methodes();
        public ScoreManager Scores = new ScoreManager("save/scores.json");

        int topScore = 5;
        int maxCountHighScores;

        public static Button StartGameButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 280, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "New Game", 30, Color.Maroon, Color.LightGray);
        public static Button MenuButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 180, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "Menu", 30, Color.Maroon, Color.LightGray);
        public static Button CloseGameButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 80, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "Quit Game", 30, Color.Maroon, Color.LightGray);
        public static Button BackToMenuButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 80, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "Back", 30, Color.Maroon, Color.LightGray);


        Color textColor = Program.greenLemon;
        Color backgroundColor = Program.darkGreen;
        public override void Load()
        {
            SceneManager.runningScene = SceneManager.enumScene.HighScores;
            if (SceneManager.previousScene == SceneManager.enumScene.Menu)
            {
                StartGameButton.isVisible = false;
                MenuButton.isVisible = false;
                CloseGameButton.isVisible = false;
                BackToMenuButton.isVisible = true;
            }
            else
            {
                StartGameButton.isVisible = true;
                MenuButton.isVisible = true;
                CloseGameButton.isVisible = true;
                BackToMenuButton.isVisible = false;
            }
            ScoreManager.HighScores.Sort();
            ScoreManager.HighScores.Reverse();
        }

        public override void Update(float deltatime)
        {
            StartGameButton.MouseHover(StartGameButton);
            MenuButton.MouseHover(MenuButton);
            BackToMenuButton.MouseHover(BackToMenuButton);
            CloseGameButton.MouseHover(CloseGameButton);
            StartGameButtonEvent();
            MenuButtonEvent();
            CloseGameButtonEvent();
        }
        public override void Draw()
        {
            Raylib.ClearBackground(backgroundColor);

            methodes.DrawCenteredText("TOP 5 HIGH SCORES", 100, 50, 4, Raylib.GetFontDefault(), textColor);
            StartGameButton.ButtonDraw();
            MenuButton.ButtonDraw();
            CloseGameButton.ButtonDraw();
            BackToMenuButton.ButtonDraw();

            maxCountHighScores = Math.Min(topScore, ScoreManager.HighScores.Count);

            int spaceY = 50;

            for (int i = 0; i < maxCountHighScores; i++)
            {
                Tuple<int, string> highscore = ScoreManager.HighScores[i];
                methodes.DrawCenteredText($"{highscore.Item2} : {highscore.Item1}", 200 + spaceY * i, 40, 4, Raylib.GetFontDefault(), textColor);
            }

        }

        public override void Unload()
        {
            StartGameButton.isVisible = false;
            MenuButton.isVisible = false;
            BackToMenuButton.isVisible = false;
            CloseGameButton.isVisible = false;
            SceneManager.previousScene = SceneManager.runningScene;
        }

        public void StartGameButtonEvent()
        {
            if (StartGameButton.isVisible && StartGameButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                StartGameButton.ButtonClic();
                SceneManager.Load<SceneGame>();
            }
        }

        public void CloseGameButtonEvent()
        {

            if (CloseGameButton.isVisible && CloseGameButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                CloseGameButton.ButtonClic();
                Program.closeGame = true;
            }
        }

        public void MenuButtonEvent()
        {

            if (((MenuButton.isVisible && MenuButton.isHover) || (BackToMenuButton.isVisible && BackToMenuButton.isHover)) && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                MenuButton.ButtonClic();
                SceneManager.Load<SceneMenu>();
            }
        }
    }
}
