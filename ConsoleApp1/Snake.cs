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
    public abstract class Snake
    {

        public static Methodes methodes = new Methodes();
        public static int snakeLenght { get; set; }
        public static List<Snake> ListBodySnake = new List<Snake>();
        public Color Color = new Color();
        private static bool checkcollision = true;
        public string Type { get; private set; }
        public int x { get; private set; }
        public int y { get; private set; }
        public int Column { get; private set; }
        public int Row { get; private set; }
        public Coordinates Coordinates { get; private set; }

        public static int Ammo=0;

        public static bool SolidHit { get; set; } = false; // check if something solid is hit and then gameover

        public static float baseSpeed = 0.0f; //snake tile to tile speed
        public static float speed = baseSpeed; //snake tile to tile speed

        public Snake(string Type, int Column, int Row)
        {
            this.Type = Type;
            this.Column = Column;
            this.Row = Row;
            this.Color = Color;
        }

        public void CreateSnakePart(Snake snakePart, int insert)
        {
            snakePart.Type = this.Type;
            snakePart.Column = this.Column;
            snakePart.Row = this.Row;
            snakePart.Color = this.Color;
            snakePart.Coordinates = new Coordinates(snakePart.Column, snakePart.Row);
            ListBodySnake.Insert(insert, snakePart);
            Game.ListOnGrid.Add(snakePart.Coordinates);
        }

        public void RemoveSnakePart(Snake snakePart)
        {
            ListBodySnake.Remove(snakePart);
            Game.ListOnGrid.Remove(snakePart.Coordinates);
        }

        public static void UpdateSnake()
        {
            Controler.UpdateDir();
            Controler.UpdateActions();
            SnakeEat();
            if (checkcollision) CheckCollision();

        }
        public static void UpdateSnakeMoves()
        {

            switch (Controler.nextDir)
            {
                case Controler.KeyboardDir.Right:
                    UpdateRight();
                    Controler.dir = Controler.nextDir;
                    checkcollision = true;
                    break;
                case Controler.KeyboardDir.Left:
                    UpdateLeft();
                    Controler.dir = Controler.nextDir;
                    checkcollision = true;
                    break;
                case Controler.KeyboardDir.Up:
                    UpdateUp();
                    Controler.dir = Controler.nextDir;
                    checkcollision = true;
                    break;
                case Controler.KeyboardDir.Down:
                    UpdateDown();
                    Controler.dir = Controler.nextDir;
                    checkcollision = true;
                    break;
                default:
                    //Console.WriteLine("no direction set");
                    break;
            }
        }
        public static void UpdateRight()
        {
            for (int i = ListBodySnake.Count - 1; i >= 0; i--)
            {
                Snake BodyPart = ListBodySnake[i];

                if (BodyPart.Column < Grid.MAPW - 1)
                {
                    if (BodyPart.Type == "head")
                    {
                        SnakeHead NewHead = new SnakeHead("head", BodyPart.Column + 1, BodyPart.Row, BodyPart.Color);
                        NewHead.CreateSnakePart(NewHead, i);
                        Game.ListOnGrid.Remove(BodyPart.Coordinates);
                        ListBodySnake.Remove(BodyPart);


                    }
                    if (BodyPart.Type == "body")
                    {
                        SnakeBody NewBody = new SnakeBody("body", ListBodySnake[i - 1].Column, ListBodySnake[i - 1].Row);
                        NewBody.CreateSnakePart(NewBody, i);
                        Game.ListOnGrid.Remove(BodyPart.Coordinates);
                        ListBodySnake.Remove(BodyPart);
                    }
                }
                else
                {
                    if (BodyPart.Type == "head")
                    {
                        SolidHit = true;
                        Console.WriteLine("Edge Hit");
                        Controler.dir = Controler.KeyboardDir.Freeze;

                    }
                }
            }
        }
        public static void UpdateLeft()
        {
            for (int i = ListBodySnake.Count - 1; i >= 0; i--)
            {
                Snake BodyPart = ListBodySnake[i];

                if (BodyPart.Column > 0)
                {
                    if (BodyPart.Type == "head")
                    {
                        SnakeHead NewHead = new SnakeHead("head", BodyPart.Column - 1, BodyPart.Row, BodyPart.Color);
                        NewHead.CreateSnakePart(NewHead, i);
                        Game.ListOnGrid.Remove(BodyPart.Coordinates);
                        ListBodySnake.Remove(BodyPart);
                    }
                    if (BodyPart.Type == "body")
                    {
                        SnakeBody NewBody = new SnakeBody("body", ListBodySnake[i - 1].Column, ListBodySnake[i - 1].Row);
                        NewBody.CreateSnakePart(NewBody, i);
                        Game.ListOnGrid.Remove(BodyPart.Coordinates);
                        ListBodySnake.Remove(BodyPart);
                    }
                }
                else
                {
                    if (BodyPart.Type == "head")
                    {
                        SolidHit = true;
                        Console.WriteLine("Edge Hit");
                        Controler.dir = Controler.KeyboardDir.Freeze;
                    }
                }
            }
        }
        public static void UpdateUp()
        {
            for (int i = ListBodySnake.Count - 1; i >= 0; i--)
            {
                Snake BodyPart = ListBodySnake[i];

                if (BodyPart.Row > 0)
                {
                    if (BodyPart.Type == "head")
                    {
                        SnakeHead NewHead = new SnakeHead("head", BodyPart.Column, BodyPart.Row - 1, BodyPart.Color);
                        NewHead.CreateSnakePart(NewHead, i);
                        Game.ListOnGrid.Remove(BodyPart.Coordinates);
                        ListBodySnake.Remove(BodyPart);
                    }
                    if (BodyPart.Type == "body")
                    {
                        SnakeBody NewBody = new SnakeBody("body", ListBodySnake[i - 1].Column, ListBodySnake[i - 1].Row);
                        NewBody.CreateSnakePart(NewBody, i);
                        Game.ListOnGrid.Remove(BodyPart.Coordinates);
                        ListBodySnake.Remove(BodyPart);
                    }
                }
                else
                {
                    if (BodyPart.Type == "head")
                    {
                        SolidHit = true;
                        Console.WriteLine("Edge Hit");
                        Controler.dir = Controler.KeyboardDir.Freeze;

                    }
                }
            }
        }
        public static void UpdateDown()
        {
            for (int i = ListBodySnake.Count - 1; i >= 0; i--)
            {
                Snake BodyPart = ListBodySnake[i];

                if (BodyPart.Row < Grid.MAPH - 1)
                {
                    if (BodyPart.Type == "head")
                    {
                        SnakeHead NewHead = new SnakeHead("head", BodyPart.Column, BodyPart.Row + 1, BodyPart.Color);
                        NewHead.CreateSnakePart(NewHead, i);
                        Game.ListOnGrid.Remove(BodyPart.Coordinates);
                        ListBodySnake.Remove(BodyPart);
                    }
                    if (BodyPart.Type == "body")
                    {
                        SnakeBody NewBody = new SnakeBody("body", ListBodySnake[i - 1].Column, ListBodySnake[i - 1].Row);
                        NewBody.CreateSnakePart(NewBody, i);
                        Game.ListOnGrid.Remove(BodyPart.Coordinates);
                        ListBodySnake.Remove(BodyPart);
                    }
                }
                else
                {
                    if (BodyPart.Type == "head")
                    {
                        SolidHit = true;
                        Console.WriteLine("Edge Hit");
                        Controler.dir = Controler.KeyboardDir.Freeze;

                    }
                }
            }
        }
        public static void SnakeEat()
        {
            for (int j = Loot.ListLoots.Count - 1; j >= 0; j--)
            {
                Loot loot = Loot.ListLoots[j];
                Coordinates VecLoot = new Coordinates(loot.column, loot.row);
                Coordinates VecSnakeHead = new Coordinates(ListBodySnake[0].Column, ListBodySnake[0].Row);

                if (VecLoot == VecSnakeHead)
                {

                    if (Game.bulletIsShot == false)
                    {
                        if (loot.type == "bullet")
                        {
                            //Console.WriteLine($"{loot.type} HIT !!");
                            loot.Effect(loot);
                        }
                        else
                        {
                            Console.WriteLine($"{loot.type} HIT !!");
                            loot.Effect(loot);
                            loot.RemoveLoot(loot);

                        }
                    }
                    else
                    {
                        if (loot.type != "bullet")
                        {
                            Console.WriteLine($"{loot.type} HIT !!");
                            loot.Effect(loot);
                            loot.RemoveLoot(loot);
                        }
                    }
                }
            }



        }
        public static void SnakeGrow()
        {
            checkcollision = false;
            SnakeBody NewBody = new SnakeBody("body", ListBodySnake[ListBodySnake.Count - 1].Column, ListBodySnake[ListBodySnake.Count - 1].Row);
            NewBody.CreateSnakePart(NewBody, ListBodySnake.Count - 1);
            if (ListBodySnake.Count > Game.maxSnakeSize) Game.maxSnakeSize++;

        }
        public static void SnakeReduce()
        {
            checkcollision = false;
            ListBodySnake.Remove(ListBodySnake[ListBodySnake.Count - 1]);
            Coordinates endOfSnake = new Coordinates(ListBodySnake[ListBodySnake.Count - 1].Column, ListBodySnake[ListBodySnake.Count - 1].Row);
            Game.ListOnGrid.Remove(endOfSnake);
        }
        public static void CheckCollision()
        {
            // CHECK IF SNAKE IS COLLIDING WITH ITSELF

            for (int i = ListBodySnake.Count - 1; i >= 0; i--)
            {
                Snake Part1 = ListBodySnake[i];
                Coordinates VecPart1 = new Coordinates(Part1.Column, Part1.Row);

                for (int j = ListBodySnake.Count - 1; j >= 0; j--)
                {
                    Snake Part2 = ListBodySnake[j];
                    Coordinates VecPart2 = new Coordinates(Part2.Column, Part2.Row);

                    if (VecPart1 == VecPart2 && i != j)
                    {
                        Controler.dir = Controler.KeyboardDir.Freeze;
                        SolidHit = true;
                    }
                }
            }

        }
        public static void SnakeShot()
        {
            Game.bulletIsShot = true;
            for (int i = Loot.ListLoots.Count - 1; i >= 0; i--)
            {
                Loot loot = Loot.ListLoots[i];
                Coordinates VecLoot = new Coordinates(loot.column, loot.row);
                Coordinates VecSnakeHead = new Coordinates(ListBodySnake[0].Column, ListBodySnake[0].Row);
                if (loot.type == "bullet")
                {
                    switch (Controler.dir)
                    {
                        case Controler.KeyboardDir.Right:
                            loot.column = ListBodySnake[0].Column + 1;
                            loot.row = ListBodySnake[0].Row;
                            loot.isVisible = true;
                            loot.speedCol = 1;
                            loot.speedRow = 0;
                            Snake.Ammo = 0;

                            break;
                        case Controler.KeyboardDir.Left:
                            loot.column = ListBodySnake[0].Column - 1;
                            loot.row = ListBodySnake[0].Row;
                            loot.isVisible = true;
                            loot.speedCol = -1;
                            loot.speedRow = 0;
                            Snake.Ammo = 0;
                            break;
                        case Controler.KeyboardDir.Up:
                            loot.column = ListBodySnake[0].Column;
                            loot.row = ListBodySnake[0].Row - 1;
                            loot.isVisible = true;
                            loot.speedCol = 0;
                            loot.speedRow = -1;
                            Snake.Ammo = 0;
                            break;
                        case Controler.KeyboardDir.Down:
                            loot.column = ListBodySnake[0].Column;
                            loot.row = ListBodySnake[0].Row + 1;
                            loot.isVisible = true;
                            loot.speedCol = 0;
                            loot.speedRow = 1;
                            Snake.Ammo = 0;
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }
}