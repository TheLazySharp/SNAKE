using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Raylib_cs.Color;

namespace Code
{
    class Hedgehog : Loot
    {
        public static bool hedgehogHit { get; set; } = false;
        public Hedgehog(string type, Color color) : base("hedgehog", Color.Brown)
        {
            
        }

        public override void Effect(Loot loot)
        {
            //Console.WriteLine("Hedgehog hit");
            Snake.SnakeReduce();
            hedgehogHit = true;
        }
    }
}
