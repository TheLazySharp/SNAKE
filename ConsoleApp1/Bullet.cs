using Code;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Color = Raylib_cs.Color;

namespace Code
{
    class Bullet : Loot
    {
        public Bullet(string type, Color Color) : base("bullet", Color.Black)
        {

        }

        public override void Effect(Loot loot)
        {
            if (loot.isVisible)
            {
                Console.WriteLine("Bullet loaded");
                Snake.Ammo++;
                loot.isVisible = false;
            }
        }
    }
}
