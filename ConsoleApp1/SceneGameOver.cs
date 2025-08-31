using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace Code
{
    public class SceneGameOver : Scene
    {
        public Methodes methodes = new Methodes();

        public static Button StartGameButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 280, 220, 50, Color.White, Color.Black, Color.LightGray, "New Game", 30, Color.White, Color.Black);
        public static Button MenuButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 180, 220, 50, Color.White, Color.Black, Color.LightGray, "Menu", 30, Color.White, Color.Black);
        public static Button CloseGameButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 80, 220, 50, Color.White, Color.Black, Color.LightGray, "Quit Game", 30, Color.White, Color.Black);
        public override void Load()
        {
            SceneManager.runningScene = SceneManager.enumScene.GameOver;
            StartGameButton.isVisible = true;
            MenuButton.isVisible = true;
            CloseGameButton.isVisible = true;
            Console.WriteLine("Loading Menu scene");
        }

        public override void Update(float deltatime)
        {
            StartGameButton.MouseHover(StartGameButton);
            MenuButton.MouseHover(CloseGameButton);
            CloseGameButton.MouseHover(CloseGameButton);
            StartGameButtonEvent();
            MenuButtonEvent();
            CloseGameButtonEvent();

            // Console.WriteLine("updating Menu scene");
        }
        public override void Draw()
        {
            Raylib.ClearBackground(Color.Black);

            methodes.DrawCenteredText("GAME OVER", 100, 50, 4, Raylib.GetFontDefault(), Color.White);
            StartGameButton.ButtonDraw();
            MenuButton.ButtonDraw();
            CloseGameButton.ButtonDraw();

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
