using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace Code
{
    public class MenuScene : Scene
    {
        public override void Load()
        {
            Console.WriteLine("Loading Menu scene");
        }

        public override void Update(float deltatime)
        {
           // Console.WriteLine("updating Menu scene");

            if (Raylib.IsKeyPressed(KeyboardKey.Space))
            {
                SceneManager.Load<GameScene>();
            }
        }

        public override void Draw()
        {
            Console.WriteLine("Drawing Menu scene");
        }

        public override void Unload()
        {
            Console.WriteLine("Unloading Menu scene");
        }

    }
}
