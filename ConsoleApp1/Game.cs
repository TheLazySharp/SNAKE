using Raylib_cs;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Color = Raylib_cs.Color;

namespace Code
{
    public class Game
    {
        Grid grid = new Grid();
        public Methodes methodes = new Methodes();
        public ScoreManager score = new ScoreManager("");
        public static List<Coordinates> ListOnGrid = new List<Coordinates>();

        private Timer HedgehogsLoot = new Timer(10f, true, spawnNewHedgehog);


        //SPEED INIT
        public float SnakeUpdateTimer { get; private set; }
        public float SnakeMaxTimer { get; private set; } = 0.25f;
        public float BulletUpdateTimer { get; private set; }
        public float BulletMaxTimer { get; private set; } = 0.3f;
        public static float BulletSpeed { get; private set; }

        //GAME SETTINGS INIT
        public int nbWall { get; private set; } = 30;
        public static int nbWallPart { get; private set; }
        public static int maxSnakeSize { get; set; }
        public static bool bulletIsShot { get; set; } = false;
        public static bool snakeHitWall { get; set; } = false;
        public static bool bulletHitWall { get; set; } = false;
        public static bool hedgehogKilled { get; set; } = false;

        public static int nbHedgeHogKilled { get; set; }

        //SNAKE INIT
        public static int snakeHeadInit { get; private set; } = 10;

        public Game()
        {

        }

        public void InitGame()
        {
            Program.nbGames++;
            SnakeUpdateTimer = 0f;
            BulletUpdateTimer = 0f;
            BulletSpeed = 0f;
            nbHedgeHogKilled = 0;
            Snake.Ammo = 0;
            Snake.SolidHit = false;
            Snake.speed = Snake.baseSpeed;
            score.InitScores();
            grid.InitGrid();

            //SNAKE INIT//

            SnakeHead Head = new SnakeHead("head", snakeHeadInit, snakeHeadInit, Color.DarkGreen);
            SnakeBody Body1 = new SnakeBody("body", snakeHeadInit - 1, snakeHeadInit);
            SnakeBody Body2 = new SnakeBody("body", snakeHeadInit - 2, snakeHeadInit);
            Head.CreateSnakePart(Head, 0);
            Body1.CreateSnakePart(Body1, 1);
            Body2.CreateSnakePart(Body2, 2);

            maxSnakeSize = 3;


            for (int i = 0; i < nbWall; i++)
            {
                Wall NewWall = new Wall();
                NewWall.CreateWall(NewWall);
                nbWallPart = Wall.ListWall.Count;
            }

            Apple NewApple = new Apple("apple", Color.Red);
            NewApple.CreateLoot(NewApple);

            Hedgehog NewHedgehog = new Hedgehog("hedgehog", Color.Brown);
            NewHedgehog.CreateLoot(NewHedgehog);

            Bullet NewBullet = new Bullet("bullet", Color.Black);
            NewBullet.CreateLoot(NewBullet);

            //END OF SNAKE INIT//

        }
        public void UpdateGame()
        {
            Snake.UpdateSnake();
            BulletImpactOnWall();
            SnakeOnWallImpact();
            BulletImpactOnHedgehog();
            score.ScoreUpdate();

            //LOOTS MANAGEMENT//

            if (Loot.ListLoots.Where(x => x != null && x.type == "apple").Count() == 0)
            {
                Apple NewApple = new Apple("apple", Color.Red);
                NewApple.CreateLoot(NewApple);
            }

            if (Loot.ListLoots.Where(x => x != null && x.type == "hedgehog").Count() == 0)
            {
                Hedgehog NewHedgehog = new Hedgehog("hedgehog", Color.Brown);
                NewHedgehog.CreateLoot(NewHedgehog);
            }

            HedgehogsLoot.UpdateTimerAtEnd(); // new hedgehog very xx sec


            if (Loot.ListLoots.Where(x => x != null && x.type == "bullet").Count() == 0)
            {
                Bullet NewBullet = new Bullet("bullet", Color.Black);
                NewBullet.CreateLoot(NewBullet);
            }

            //SPEED MANAGEMENT

            BulletSpeed = (Snake.speed + 0.1f) * 1.1f;
            if (BulletSpeed >= BulletMaxTimer) BulletSpeed = BulletMaxTimer;
            if (Snake.speed >= SnakeMaxTimer) Snake.speed = SnakeMaxTimer;


            SnakeUpdateTimer += Raylib.GetFrameTime();
            BulletUpdateTimer += Raylib.GetFrameTime();

            if (SnakeUpdateTimer > SnakeMaxTimer - Snake.speed)
            {
                Snake.UpdateSnakeMoves();
                SnakeUpdateTimer = 0;
            }

            if (BulletUpdateTimer > BulletMaxTimer - BulletSpeed)
            {
                for (int i = Loot.ListLoots.Count - 1; i >= 0; i--)
                {
                    Loot loot = Loot.ListLoots[i];
                    if (loot.type == "bullet")
                    {
                        loot.column = loot.column + loot.speedCol;
                        loot.row = loot.row + loot.speedRow;
                        loot.coordinates = new Coordinates(loot.column, loot.row);

                        if (loot.column > Grid.MAPW - 1 || loot.column < 0 || loot.row < 0 || loot.row > Grid.MAPH - 1)
                        {
                            loot.RemoveLoot(loot);
                            Game.bulletIsShot = false;
                        }
                    }
                }
                BulletUpdateTimer = 0;
            }
        }
        public void BulletImpactOnWall()
        {
            for (int i = Loot.ListLoots.Count - 1; i >= 0; i--)
            {
                Loot loot = Loot.ListLoots[i];
                if (loot.type == "bullet")
                {
                    for (int j = Wall.ListWall.Count - 1; j >= 0; j--)
                    {
                        Wall wall = Wall.ListWall[j];

                        if (loot.coordinates == wall.coordinates)
                        {
                            Console.WriteLine("Wall hit");
                            AudioManager.PlaySound(AudioManager.wallDestroyed);
                            bulletHitWall = true;
                            loot.RemoveLoot(loot);
                            wall.RemoveWall(wall);
                            Game.bulletIsShot = false;
                        }
                    }
                }
            }
        }
        public void BulletImpactOnHedgehog()
        {
            for (int i = Loot.ListLoots.Count - 1; i >= 0; i--)
            {
                Loot bullet = Loot.ListLoots[i];

                if (bullet.type == "bullet")
                {
                    for (int j = Loot.ListLoots.Count - 1; j >= 0; j--)
                    {
                        Loot target = Loot.ListLoots[j];
                        if (target.type == "hedgehog")
                        {
                            if (bullet.coordinates == target.coordinates)
                            {
                                Console.WriteLine("Hedgehod killed");
                                AudioManager.PlaySound(AudioManager.hedgehogKilled);
                                hedgehogKilled = true;
                                nbHedgeHogKilled++;
                                target.RemoveLoot(target);
                                bullet.RemoveLoot(bullet);
                                Game.bulletIsShot = false;
                                return;
                            }
                        }
                    }
                }
            }
        }

        public void SnakeOnWallImpact()
        {
            for (int j = Wall.ListWall.Count - 1; j >= 0; j--)
            {
                Wall wall = Wall.ListWall[j];
                Coordinates SnakeHead = new Coordinates(Snake.ListBodySnake[0].Column, Snake.ListBodySnake[0].Row);

                if (SnakeHead == wall.coordinates)
                {
                    AudioManager.PlaySound(AudioManager.wallHit);
                    if (Snake.Ammo > 0)
                    {
                        for (int i = Loot.ListLoots.Count - 1; i >= 0; i--)
                        {
                            Loot loot = Loot.ListLoots[i];
                            if (loot.type == "bullet")
                            {
                                loot.RemoveLoot(loot);
                                Snake.Ammo = 0;
                            }
                        }
                    }
                    wall.RemoveWall(wall);
                    Console.WriteLine("Wall Hit");
                    snakeHitWall = true;
                    for (int i = Snake.ListBodySnake.Count - 1; i > 2; i--)
                    {
                        Snake SnakePart = Snake.ListBodySnake[i];
                        Snake.ListBodySnake.Remove(SnakePart);
                        Coordinates snakeOnGrid = new Coordinates(SnakePart.Column, SnakePart.Row);
                        Game.ListOnGrid.Remove(snakeOnGrid);

                    }
                    Snake.GetSnakeHeadPos(Controler.nextDir); // avoid mismatch betwenn head and tail's dirs
                    while (Snake.HeadDir.Count > 1) Snake.HeadDir.Dequeue();
                }
            }
        }

        private static void spawnNewHedgehog()
        {
            int nbHedgehogs = Loot.ListLoots.Where(x => x != null && x.type == "hedgehog").Count();
            if (nbHedgehogs < 10)
            {
                Hedgehog NewHedgehog = new Hedgehog("hedgehog", Color.Brown);
                NewHedgehog.CreateLoot(NewHedgehog);
            }

        }


    }
}
