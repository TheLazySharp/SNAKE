using Raylib_cs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Code
{
    public static class TextureManager
    {
        public static Texture2D upArrow;
        public static Texture2D downArrow;
        public static Texture2D leftArrow;
        public static Texture2D rightArrow;
        public static Texture2D spaceBar;
        public static Texture2D qKey;
        public static Texture2D hedgehog;
        public static Texture2D apple;
        public static Texture2D venom;
        public static Texture2D poison;
        public static Texture2D snakeHeadR;
        public static Texture2D snakeHeadB;
        public static Texture2D snakeHeadL;
        public static Texture2D snakeHeadU;
        public static Texture2D snakeHeadLoadedR;
        public static Texture2D snakeHeadLoadedB;
        public static Texture2D snakeHeadLoadedL;
        public static Texture2D snakeHeadLoadedU;
        public static Texture2D snakeTailR;
        public static Texture2D snakeTailB;
        public static Texture2D snakeTailL;
        public static Texture2D snakeTailU;
        public static Texture2D wall;

        public static void LoadTextures()
        {
            upArrow = Raylib.LoadTexture("images/COMMAND_UP.png");
            downArrow = Raylib.LoadTexture("images/COMMAND_DOWN.png");
            leftArrow = Raylib.LoadTexture("images/COMMAND_L.png");
            rightArrow = Raylib.LoadTexture("images/COMMAND_R.png");
            spaceBar = Raylib.LoadTexture("images/COMMAND_SPACE.png");
            qKey = Raylib.LoadTexture("images/COMMAND_Q.png");
            hedgehog = Raylib.LoadTexture("images/HEDGEHOG.png");
            apple = Raylib.LoadTexture("images/APPLE.png");
            venom = Raylib.LoadTexture("images/VENOM.png");
            poison = Raylib.LoadTexture("images/POISON.png");
            snakeHeadR = Raylib.LoadTexture("images/SNAKE_HEAD_R.png");
            snakeHeadB = Raylib.LoadTexture("images/SNAKE_HEAD_B.png");
            snakeHeadL = Raylib.LoadTexture("images/SNAKE_HEAD_L.png");
            snakeHeadU = Raylib.LoadTexture("images/SNAKE_HEAD_U.png");
            snakeHeadLoadedR = Raylib.LoadTexture("images/SNAKE_HEAD_LOADED_R.png");
            snakeHeadLoadedB = Raylib.LoadTexture("images/SNAKE_HEAD_LOADED_B.png");
            snakeHeadLoadedL = Raylib.LoadTexture("images/SNAKE_HEAD_LOADED_L.png");
            snakeHeadLoadedU = Raylib.LoadTexture("images/SNAKE_HEAD_LOADED_U.png");
            snakeTailR = Raylib.LoadTexture("images/SNAKE_TAIL_R.png");
            snakeTailB = Raylib.LoadTexture("images/SNAKE_TAIL_B.png");
            snakeTailL = Raylib.LoadTexture("images/SNAKE_TAIL_L.png");
            snakeTailU = Raylib.LoadTexture("images/SNAKE_TAIL_U.png");
            wall = Raylib.LoadTexture("images/BRICK.png");
        }
        
        public static void Awake(string textureName)
        {
            Texture2D Texture = Raylib.LoadTexture($"images/{textureName}.png");
            Raylib.UnloadTexture(Texture);
        }
    }
}


