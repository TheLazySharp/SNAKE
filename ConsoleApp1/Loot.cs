using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Color = Raylib_cs.Color;

namespace Code
{
    public abstract class Loot
    {
        public static List<Loot> ListLoots = new List<Loot>();
        public Color color = new Color();
        public int column { get; set; }
        public int row { get; set; }
        public string type { get; private set; }
        public int speedCol { get; set; }
        public int speedRow { get; set; }
        public int speed { get; set; }
        public bool isVisible { get; set; }
        public Coordinates coordinates { get; set; }
        Random random = new Random();

        public Loot(string type, Color color)
        {
            this.type = type;
            this.color = color;
        }

        public void CreateLoot(Loot loot)
        {
            Random random = new Random();
            loot.column = random.Next(0, Grid.MAPW - 1);
            loot.row = random.Next(0, Grid.MAPH - 1);
            loot.coordinates = new Coordinates(loot.column, loot.row);

            if (Game.ListOnGrid.Contains(loot.coordinates))
            {
                while (Game.ListOnGrid.Contains(loot.coordinates))
                    {
                        loot.column = random.Next(0, Grid.MAPW - 1);
                        loot.row = random.Next(0, Grid.MAPH - 1);
                        loot.coordinates = new Coordinates(loot.column, loot.row);
                        if (!Game.ListOnGrid.Contains(loot.coordinates)) break;
                    }
            }


            loot.color = this.color;
            loot.isVisible = true;
            loot.speedCol = 0;
            loot.speedRow = 0;
            ListLoots.Add(loot);
            Game.ListOnGrid.Add(loot.coordinates);
            Console.WriteLine($"{loot.type} created at Column {loot.column} and Row {loot.row}");
        }

        public abstract void Effect(Loot loot);

        public void RemoveLoot(Loot loot)
        {
            Game.ListOnGrid.Remove(loot.coordinates);
            ListLoots.Remove(loot);
        }

    }
}
