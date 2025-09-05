using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Raylib_cs.Color;

namespace Code
{
    class Apple : Loot
    {
        public static bool appleEat { get; set; } = false;
        public Apple(string type, Color Color) : base("apple", Color.Red)
        {
            
        }

        public override void Effect(Loot loot)
        {
            Console.WriteLine("Apple eaten");
            AudioManager.PlaySound(AudioManager.grow);
            Snake.SnakeGrow();
            appleEat = true;
        }
    }
}
