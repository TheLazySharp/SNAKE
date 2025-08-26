using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public static class SceneManager
    {
        private static Scene? currentScene;

        public static void Load<T>() where T : Scene, new() // mon type générique sera forcément une scene ( where T : Scene) et je veux l'instancier moi même ( new;)
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
