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
        public enum KeyboardDir { Right, Left, Up, Down, Freeze }
        public static KeyboardDir dir = KeyboardDir.Freeze;
        public static KeyboardDir nextDir;
        public static bool AmmoLoaded { private set; get; } = false;

        public static void UpdateDir()
        {
        
            if(Raylib.IsKeyDown(KeyboardKey.Right) && dir != KeyboardDir.Left) dir = KeyboardDir.Right; 
            if(Raylib.IsKeyDown(KeyboardKey.Left) && dir !=KeyboardDir.Freeze && dir !=KeyboardDir.Right) dir = KeyboardDir.Left;
            if(Raylib.IsKeyDown(KeyboardKey.Up) && dir != KeyboardDir.Down) dir = KeyboardDir.Up;
            if(Raylib.IsKeyDown(KeyboardKey.Down) && dir != KeyboardDir.Up) dir = KeyboardDir.Down;

            nextDir = dir;
        }


        //public static void UpdateDir()
        //{
        //    if (Raylib.IsKeyPressed(KeyboardKey.Right)
        //        && dir != KeyboardDir.Left
        //        && Raylib.IsKeyUp(KeyboardKey.Left)
        //        && Raylib.IsKeyUp(KeyboardKey.Down)
        //        && Raylib.IsKeyUp(KeyboardKey.Up)
        //        )
        //    {
        //        Console.WriteLine("Direction : Right");
        //        dir = KeyboardDir.Right;
        //    }
        //    if (Raylib.IsKeyPressed(KeyboardKey.Left)
        //        && dir != KeyboardDir.Right
        //        && dir != KeyboardDir.Freeze
        //        && Raylib.IsKeyUp(KeyboardKey.Right)
        //        && Raylib.IsKeyUp(KeyboardKey.Down)
        //        && Raylib.IsKeyUp(KeyboardKey.Up)
        //        )
        //    {
        //        Console.WriteLine("Direction : Left");
        //        dir = KeyboardDir.Left;
        //    }
        //    if (Raylib.IsKeyPressed(KeyboardKey.Up)
        //        && dir != KeyboardDir.Down
        //        && Raylib.IsKeyUp(KeyboardKey.Left)
        //        && Raylib.IsKeyUp(KeyboardKey.Down)
        //        && Raylib.IsKeyUp(KeyboardKey.Right)
        //        )
        //    {
        //        Console.WriteLine("Direction : Up");
        //        dir = KeyboardDir.Up;
        //    }
        //    if (Raylib.IsKeyPressed(KeyboardKey.Down)
        //        && dir != KeyboardDir.Up
        //        && Raylib.IsKeyUp(KeyboardKey.Left)
        //        && Raylib.IsKeyUp(KeyboardKey.Right)
        //        && Raylib.IsKeyUp(KeyboardKey.Up)
        //        )
        //    {
        //        Console.WriteLine("Direction : Down");
        //        dir = KeyboardDir.Down;
        //    }
        //    if (Raylib.IsKeyPressed(KeyboardKey.Tab))
        //    {
        //        Console.WriteLine("Direction : Freeze");
        //        dir = KeyboardDir.Freeze;
        //    }

        //}

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
