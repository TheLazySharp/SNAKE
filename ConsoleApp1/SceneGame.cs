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
        public static Drawer drawer = new Drawer();
        public static bool GameOnPause;


        public override void Load()
        {
            SceneManager.runningScene = SceneManager.enumScene.Game;
            AudioManager.PlayMusic(AudioManager.gameMusic);
            if (SceneManager.previousScene == SceneManager.enumScene.Pause)
            {
                GameOnPause = false;
                return;
            }
            else
            {
                game.InitGame();
            }

        }

        public override void Update(float deltatime)
        {
            AudioManager.UpdateMusic(AudioManager.gameMusic);
            
            if (!GameOnPause) game.UpdateGame();

            if (Raylib.IsKeyPressed(KeyboardKey.Q) && !GameOnPause)
            {
                SceneManager.nextScene = SceneManager.enumScene.Pause;
                Console.WriteLine("Pause ON");
                GameOnPause = true;
                SceneManager.Load<ScenePauseGame>();


            }
            //HOW TO WIN -- NOT USED
            //if (!GameOnPause && Wall.ListWall.Count == 0)
            //{
            //    SceneManager.Load<SceneVictory>();
            //}

            //HOW TO LOSE
            if (Snake.SolidHit || Snake.ListBodySnake.Count == 2)
            {
                SceneManager.Load<SceneGameOver>();
            }

            //Transition from pause to menu to unload game scene
            if (SceneManager.nextScene == SceneManager.enumScene.Menu && SceneManager.previousScene == SceneManager.enumScene.Pause)
            {
                SceneManager.Load<SceneMenu>();
            }

        }

        public override void Draw()
        {
            drawer.Draw();
        }

        public override void Unload()
        {

            if (SceneManager.nextScene == SceneManager.enumScene.Pause)
            {
                return;
            }
            else
            {
                AudioManager.StopMusic(AudioManager.gameMusic);
                Game.ListOnGrid.Clear();
                Snake.ListBodySnake.Clear();
                Snake.HeadDir.Clear();
                Loot.ListLoots.Clear();
                Wall.ListWall.Clear();
                ScoreManager.QueueScores.Clear();
                Controler.nextDir = Controler.KeyboardDir.Start;
                SceneManager.previousScene = SceneManager.runningScene;
                Console.WriteLine("unloading game");
            }
        }

    }
}
