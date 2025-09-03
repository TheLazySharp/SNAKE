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
    public class TextureCollector
    {
        private string textureName;

        public void Awake(string textureName)
        {
            Texture2D Texture = Raylib.LoadTexture($"images/{textureName}.png");
            Raylib.UnloadTexture(Texture);

        }
    }
}


