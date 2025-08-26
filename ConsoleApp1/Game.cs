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
        public static List<Coordinates> ListOnGrid = new List<Coordinates>();
        
        //SPEED INIT
        public float SnakeUpdateTimer { get; private set; } = 0f;
        public float SnakeMaxTimer { get; private set; } = 0.25f;
        public float BulletUpdateTimer { get; private set; } = 0f;
        public float BulletMaxTimer { get; private set; } = 0.3f;
        public static float BulletSpeed { get; private set; }

        //GAME SETTINGS INIT
        public int nbWall { get; private set; } = 30;
        public static int nbWallPart { get; private set; }
        public static int maxSnakeSize { get; set; }

        //SCORE SETTINGS

        public static float score = 0f;
        public static float scoreMultiplier { get; private set; } = 0f;
        public static int nbAppleToEatToIncreaseSpeed { get; private set; } 
        public static int nbAppleEaten { get; private set; } = 0;
        public static int appleInARow { get; private set; } = 0;
        public static bool snakeHitWall { get; private set; } = false;
        public static bool hedgehogKilled { get; private set; } = false;
        public static bool bulletIsShot { get; set; } = false;

        //SNAKE INIT
        public static int snakeHeadInit { get; private set; } = 10;

        public void InitGame()
        {
            grid.InitGrid();

            //SNAKE INIT//

            SnakeHead Head = new SnakeHead("head", snakeHeadInit, snakeHeadInit, Color.DarkGreen);
            SnakeBody Body1 = new SnakeBody("body", snakeHeadInit - 1, snakeHeadInit, Color.DarkGreen);
            SnakeBody Body2 = new SnakeBody("body", snakeHeadInit - 2, snakeHeadInit, Color.DarkGreen);
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
            ScoreManager();

            //LOOTS MANAGEMENT//

            if (Loot.ListLoots.Count == 0) //prevent some rare situation where the methodes are out of range
            {
                Apple NewApple = new Apple("apple", Color.Red);
                NewApple.CreateLoot(NewApple);
            }
            
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

            if (Loot.ListLoots.Where(x => x != null && x.type == "bullet").Count() == 0)
            {
                Bullet NewBullet = new Bullet("bullet", Color.Black);
                NewBullet.CreateLoot(NewBullet);
            }

            //SPEED MANAGEMENT

            if (BulletSpeed >= BulletMaxTimer) BulletSpeed = BulletMaxTimer;
            if (Snake.speed >= SnakeMaxTimer) Snake.speed = SnakeMaxTimer;
            BulletSpeed = (Snake.speed + 0.1f) * 1.1f;

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
                                hedgehogKilled = true;

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
                    wall.RemoveWall(wall);
                    snakeHitWall = true;
                    for (int i = Snake.ListBodySnake.Count - 1; i > 1; i--)
                    {
                        Snake SnakePart = Snake.ListBodySnake[i];
                        Snake.ListBodySnake.Remove(SnakePart);
                        
                    }
                }
            }
        }

        public void ScoreManager()
        {
            nbAppleToEatToIncreaseSpeed = 5;


            if (nbAppleEaten >= nbAppleToEatToIncreaseSpeed)
            {
                Snake.speed = nbAppleEaten / nbAppleToEatToIncreaseSpeed*0.02f;
            }

            scoreMultiplier = (1 + appleInARow / nbAppleToEatToIncreaseSpeed * 0.5f);

            if (score < 0) score = 0f;
            if (Apple.appleEat)
            {
                score += 100*scoreMultiplier;
                nbAppleEaten++;
                appleInARow++;

                Apple.appleEat = false;
            }

            if (Hedgehog.hedgehogHit)
            {
                score -= 300 * scoreMultiplier;
                appleInARow = 0;
                Hedgehog.hedgehogHit = false;
            }
            if (snakeHitWall)
            {
                score -= 50 * scoreMultiplier;
                appleInARow = 0;
                snakeHitWall = false;
            }
            if (hedgehogKilled)
            {
                score += 200 * scoreMultiplier;
                hedgehogKilled = false;
            }
        }
    }
}
