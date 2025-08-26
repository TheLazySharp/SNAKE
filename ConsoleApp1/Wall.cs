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
    public class Wall
    {
        public int size { get; private set; }
        public static int maxSize { get; private set; } = 4;
        public int column { get; private set; }
        public int row { get; private set; }
        public bool isVisible { get; set; }
        public bool vertical { get; set; }

        public Coordinates coordinates { get; private set; }
        public static List<Wall> ListWall = new List<Wall>();
        public Color color = new Color();
        Random random = new Random();
        Methodes methodes = new Methodes();

        public Wall()
        {
            //this.size = size;
            //this.Coordinates = coordinates;
            //this.color = Color.DarkGray;
        }

        public void CreateWall(Wall wall)
        {
            Random random = new Random();
            wall.column = random.Next(0, Grid.MAPW - maxSize);
            wall.row = random.Next(0, Grid.MAPH - maxSize);
            if (methodes.IsWallOnSnake(wall)) // 3 == snake body part creted at init
            {
                while (methodes.IsWallOnSnake(wall))
                {
                    wall.column = random.Next(0, Grid.MAPW - maxSize);
                    wall.row = random.Next(0, Grid.MAPH - maxSize);

                    if (methodes.IsWallOnSnake(wall) == false) break;
                }
            }
            wall.size = random.Next(2, maxSize);
            wall.vertical = random.Next(2) == 1;
            wall.color = Color.Gray;
            wall.isVisible = true;
            wall.coordinates = new Coordinates(wall.column, wall.row);
            ListWall.Add(wall);
            Game.ListOnGrid.Add(wall.coordinates);
            Console.WriteLine($"Wall created at Column {wall.column} and Row {wall.row} of size {wall.size}");
            for (int i = 1; i < wall.size; i++)
            {

                if (wall.vertical)
                {
                    Wall wall2 = new Wall();
                    wall2.isVisible = true;
                    wall2.size = 1;
                    wall2.column = wall.column;
                    wall2.row = wall.row + i;
                    wall2.coordinates = new Coordinates(wall2.column, wall2.row);
                    wall2.color = wall.color;
                    ListWall.Add(wall2);
                    Game.ListOnGrid.Add(wall2.coordinates);

                    Console.WriteLine($"Wall Vert added at Column {wall2.column} and Row {wall2.row}");
                }
                else
                {
                    Wall wall2 = new Wall();
                    wall2.isVisible = true;
                    wall2.column = wall.column + i;
                    wall2.row = wall.row;
                    wall2.coordinates = new Coordinates(wall2.column, wall2.row);
                    wall2.size = 1;
                    wall2.color = wall.color;
                    ListWall.Add(wall2);
                    Game.ListOnGrid.Add(wall2.coordinates);
                    Console.WriteLine($"Wall Hor added at Column {wall2.column} and Row {wall2.row}");
                }

            }
        }
        public void RemoveWall(Wall wall)
        {
            Game.ListOnGrid.Remove(wall.coordinates);
            ListWall.Remove(wall);
        }
    }
}
