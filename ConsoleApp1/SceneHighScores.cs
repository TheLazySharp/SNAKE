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

        int topScore = 5;
        int maxCountHighScores;

        public static Button StartGameButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 280, 220, 50, Color.White, Color.Black, Color.LightGray, "New Game", 30, Color.White, Color.Black);
        public static Button MenuButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 180, 220, 50, Color.White, Color.Black, Color.LightGray, "Menu", 30, Color.White, Color.Black);
        public static Button CloseGameButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 80, 220, 50, Color.White, Color.Black, Color.LightGray, "Quit", 30, Color.White, Color.Black);

        public override void Load()
        {
            SceneManager.runningScene = SceneManager.enumScene.HighScores;
            StartGameButton.isVisible = true;
            MenuButton.isVisible = true;
            CloseGameButton.isVisible = true;
           
            SceneVictory.HighScores.Sort();
            SceneVictory.HighScores.Reverse();
        }

        public override void Update(float deltatime)
        {
            StartGameButton.MouseHover(StartGameButton);
            MenuButton.MouseHover(MenuButton);
            CloseGameButton.MouseHover(CloseGameButton);
            StartGameButtonEvent();
            MenuButtonEvent();
            CloseGameButtonEvent();



            // Console.WriteLine("updating Menu scene");
        }
        public override void Draw()
        {
            Raylib.ClearBackground(Color.Black);

            methodes.DrawCenteredText("TOP 5 HIGH SCORES", 100, 50, 4, Raylib.GetFontDefault(), Color.White);
            StartGameButton.ButtonDraw();
            MenuButton.ButtonDraw();
            CloseGameButton.ButtonDraw();

            maxCountHighScores = Math.Min(topScore, SceneVictory.HighScores.Count);

            int spaceY = 50;

            for (int i = 0; i < maxCountHighScores; i++)
            {
                Tuple<int, string> highscore = SceneVictory.HighScores[i];
                methodes.DrawCenteredText($"{highscore.Item2} - {highscore.Item1}", 200 + spaceY * i, 40, 4, Raylib.GetFontDefault(), Color.White);
            }

            //Console.WriteLine("Drawing Menu scene");
        }

        public override void Unload()
        {
            StartGameButton.isVisible = false;
            MenuButton.isVisible = false;
            CloseGameButton.isVisible = false;
            Console.WriteLine("Unloading Menu scene");
        }

        public void StartGameButtonEvent()
        {
            if (StartGameButton.isVisible && StartGameButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                SceneManager.Load<SceneGame>();
            }
        }

        public void CloseGameButtonEvent()
        {
            if (CloseGameButton.isVisible && CloseGameButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Program.closeGame = true;
            }
        }

        public void MenuButtonEvent()
        {
            if (MenuButton.isVisible && MenuButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                SceneManager.Load<SceneMenu>();
            }
        }
    }
}
