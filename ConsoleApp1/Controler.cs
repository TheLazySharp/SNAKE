using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public static class Controler
    {
        public enum KeyboardDir { Right, Left, Up, Down, Freeze, Start }
        public static KeyboardDir dir;
        public static KeyboardDir nextDir = KeyboardDir.Start;
        public static bool AmmoLoaded { private set; get; } = false;



        public static void UpdateDir()
        {
            if (Raylib.IsKeyDown(KeyboardKey.Right) && dir != KeyboardDir.Left) nextDir = KeyboardDir.Right;
            if (Raylib.IsKeyDown(KeyboardKey.Left) && dir != KeyboardDir.Freeze && dir != KeyboardDir.Right) nextDir = KeyboardDir.Left;
            if (Raylib.IsKeyDown(KeyboardKey.Up) && dir != KeyboardDir.Down) nextDir = KeyboardDir.Up;
            if (Raylib.IsKeyDown(KeyboardKey.Down) && dir != KeyboardDir.Up) nextDir = KeyboardDir.Down;

        }

        public static void UpdateActions()
        {

            if (Raylib.IsKeyPressed(KeyboardKey.Space))
            {
                if (Snake.Ammo > 0)
                {
                    Console.WriteLine("Snake Shot !!");
                    Snake.SnakeShot();
                }
                else
                {
                    return;
                }
            }

        }
    }
}
