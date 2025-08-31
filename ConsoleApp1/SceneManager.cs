using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public static class SceneManager
    {
        public static Scene? currentScene;
        
        public enum enumScene
        {
            None,
            Title,
            Menu,
            Game,
            Victory,
            HighScores,
            GameOver
        }

        public static enumScene runningScene;

        public static void Load<T>() where T : Scene, new() // mon type générique sera forcément un enfant de scene ( where T : Scene) et je veux l'instancier moi même ( new;)
        {
            currentScene?.Unload(); // comme current scene est nullable on va l'executer que s'il n'est pas null
            currentScene = new T(); // j'instancie une nouvelle scene
            currentScene.Load(); 
        }

        public static void Update(float deltatime)
        {
            currentScene?.Update(deltatime);
        }

        public static void Draw()
        {
            currentScene?.Draw();
        }

    }
}
