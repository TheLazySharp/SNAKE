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

        public static Button StartGameButton = new Button((int)(Program.ScreenW * 0.5f-110), Program.ScreenH - 180, 220, 50, Color.White, Color.Black, Color.LightGray, "New Game", 30, Color.White, Color.Black);
        public static Button CloseGameButton = new Button((int)(Program.ScreenW * 0.5f-110), Program.ScreenH - 80, 220, 50, Color.White, Color.Black, Color.LightGray, "Quit Game", 30, Color.White, Color.Black);
        public override void Load()
        {
            SceneManager.runningScene = SceneManager.enumScene.Menu;
            StartGameButton.isVisible = true;
            CloseGameButton.isVisible = true;
            Console.WriteLine("Loading Menu scene");
        }

        public override void Update(float deltatime)
        {
            StartGameButton.MouseHover(StartGameButton);
            CloseGameButton.MouseHover(CloseGameButton);
            StartGameButtonEvent();
            CloseGameButtonEvent();

            // Console.WriteLine("updating Menu scene");
        }
        public override void Draw()
        {
            Raylib.ClearBackground(Color.Black);

            methodes.DrawCenteredText("SNAKE SURVIVOR", 100, 50, 4, Raylib.GetFontDefault(), Color.White);
            StartGameButton.ButtonDraw();
            CloseGameButton.ButtonDraw();

            //Console.WriteLine("Drawing Menu scene");
        }

        public override void Unload()
        {
            StartGameButton.isVisible = false;
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
    }
}
