using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class SceneGame : Scene
    {
        public Game game = new Game();
        //public PauseGame pause = new PauseGame();
        public static Drawer drawer = new Drawer();
        public static bool GameOnPause;
        private ScenePauseGame pause = new ScenePauseGame();
        
        public override void Load()
        {
            SceneManager.runningScene = SceneManager.enumScene.Game;
            game.InitGame();
            GameOnPause = false;
        }

        public override void Update(float deltatime)
        {
            if (!GameOnPause) game.UpdateGame();
            if (Raylib.IsKeyPressed(KeyboardKey.Tab) && !GameOnPause)
            {
                Console.WriteLine("Pause ON");
                GameOnPause = true;

            }
            if (GameOnPause)
            {
                pause.LoadPause();
                pause.UpdatePause();
            }

            //HOW TO WIN
            if (!GameOnPause &&  Wall.ListWall.Count ==0)
            {
                SceneManager.Load<SceneVictory>();
            }

            //HOW TO LOSE
            if (Snake.SolidHit || Snake.ListBodySnake.Count == 1)
            {
                SceneManager.Load<SceneGameOver>();

            }

        }

        public override void Draw()
        {
            drawer.Draw();
        }

        public override void Unload()
        {
            Game.ListOnGrid.Clear();
            Snake.ListBodySnake.Clear();
            Loot.ListLoots.Clear();
            Wall.ListWall.Clear();
            ScoreManager.QueueScores.Clear();
            Controler.nextDir = Controler.KeyboardDir.Freeze;
            
            //Console.WriteLine("Unloading Game scene");
        }

    }
}
