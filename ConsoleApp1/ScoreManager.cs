using Code;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Color = Raylib_cs.Color;
using System.Diagnostics.Metrics;

namespace Code
{
    public class ScoreManager
    {
        public Methodes methodes = new Methodes();
        //SCORE SETTINGS

        public static int score { get; private set; }
        public static int nbAppleToEatToIncreaseSpeed { get; private set; } = 5;
        public static float scoreMultiplier { get; private set; } 
        public static int nbAppleEaten { get; private set; }
        public static int appleInARow { get; private set; }

        public static List<Tuple<int, string>> HighScores = new List<Tuple<int, string>>();

        private float deltatime = Raylib.GetFrameTime();    
        Timer popingScoreTimer = new Timer(1f, true, onTimerTriggered);

        public static Queue<Vector3> QueueScores = new Queue<Vector3>();

        public ScoreManager()
        {

        }

        public void InitScores()
        {
            score = 0;
            nbAppleEaten = 0;
            appleInARow = 0;
        }

        public static void onTimerTriggered()
        {
            if (QueueScores.Count > 0) QueueScores.Dequeue(); else return;
            Console.WriteLine("end of pop timer");
        }
        public void ScoreUpdate()
        {
            popingScoreTimer.UpdateTimerAtEnd();

            scoreMultiplier  = (1 + (appleInARow + Snake.ListBodySnake.Count-2)*0.02f)* (1+Snake.speed);

            if (nbAppleEaten >= nbAppleToEatToIncreaseSpeed)
            {
                Snake.speed = nbAppleEaten / nbAppleToEatToIncreaseSpeed * 0.02f;
            }
            if (score < 0) score = 0;

            if (Apple.appleEat)
            {
                int newScore = (int)(100 * scoreMultiplier);
                score += newScore;
                nbAppleEaten++;
                appleInARow++;
                PopScore(newScore);
                Apple.appleEat = false;
                Console.WriteLine($"score : {newScore}");
            }

            if (Hedgehog.hedgehogHit)
            {
                int newScore = (int)(-300 * scoreMultiplier);
                score += newScore;
                appleInARow = 0;
                PopScore(newScore);
                Hedgehog.hedgehogHit = false;
                Console.WriteLine($"score : {newScore}");
            }
            if (Game.snakeHitWall)
            {
                int newScore = (int)(-50 * scoreMultiplier);
                score += newScore;
                appleInARow = 0;
                PopScore(newScore);
                Game.snakeHitWall = false;
                Console.WriteLine($"score : {newScore}");
            }
            if (Game.hedgehogKilled)
            {
                int newScore = (int)(200 * scoreMultiplier);
                score += newScore;
                PopScore(newScore);
                Game.hedgehogKilled = false;
                Console.WriteLine($"score : {newScore}");
            }
            if (Game.bulletHitWall)
            {
                int newScore = (int)(50 * scoreMultiplier);
                score += newScore;
                PopScore(newScore);
                Game.bulletHitWall = false;
                Console.WriteLine($"score : {newScore}");
            }
        }

        public void PopScore(int newScore)
        {
            int column = Snake.ListBodySnake[0].Column;
            int row = Snake.ListBodySnake[0].Row;
            (int x, int y) hit = methodes.GridToWorld(column, row);
            Vector3 vScore = new Vector3(hit.x, hit.y, newScore);
            QueueScores.Enqueue(vScore);
            QueueScores.Contains()
        }

        public static void DrawPopingScores()
        {
            foreach (Vector3 vScore in QueueScores)
            {
                int x = (int)vScore.X;
                int y = (int)vScore.Y;
                int zScore = (int)vScore.Z;
                Color color;
                if (zScore < 0) color = Color.Orange; else color = Color.DarkGreen;
                Raylib.DrawText($"{zScore}", x, y, 30, color);
            }
        }
    }
}
