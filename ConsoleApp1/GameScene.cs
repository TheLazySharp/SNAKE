using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class GameScene : Scene
    {
        public Game game = new Game();
        //public PauseGame pause = new PauseGame();
        public static Drawer drawer = new Drawer();
        public static bool GameOnPause;
        private PauseGame pause = new PauseGame();
        public override void Load()
        {
            game.InitGame();
            GameOnPause = false;
        }

        public override void Update(float deltatime)
        {
            if (!GameOnPause) game.UpdateGame();
            if (Raylib.IsKeyPressed(KeyboardKey.Tab) && !GameOnPause)
            {
                Console.WriteLine("Pause ON");
                GameOnPause = true;

            }
            if (GameOnPause)
            {
                pause.LoadPause();
                pause.UpdatePause();
            }

        }

        public override void Draw()
        {
            drawer.Draw();
        }

        public override void Unload()
        {
            //Console.WriteLine("Unloading Game scene");
        }

    }
}
