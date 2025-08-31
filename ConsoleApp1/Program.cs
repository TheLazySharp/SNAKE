using Code;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using Color = Raylib_cs.Color;


public class Program
{
    public static Grid grid = new Grid();
    //public static Snake snake = new Snake();
    public static Drawer drawer = new Drawer();
    public static Game game = new Game();
    public readonly static int ScreenW = 1024;
    public readonly static int ScreenH = 768;
    public static bool closeGame = false;

    public static Task Main(string[] args)
    {
        Raylib.InitWindow(ScreenW, ScreenH,"SNAKE SURVIVOR");
        Raylib.SetTargetFPS(60);
        //game.InitGame();

        SceneManager.previousScene = SceneManager.enumScene.None;
        SceneManager.runningScene = SceneManager.enumScene.None;
        SceneManager.nextScene = SceneManager.enumScene.None;
        SceneManager.Load<SceneMenu>();

        while (!Raylib.WindowShouldClose())
        {
       
            SceneManager.Update(Raylib.GetFrameTime());

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            SceneManager.Draw();

            //=================== DEBUG ======================= //

            Raylib.DrawText($" Current Scene : {SceneManager.runningScene} || Previous Scene : {SceneManager.previousScene} || Next Scene : {SceneManager.nextScene}",1,15,10,Color.Black);
            Raylib.DrawText($" Current Scene : {SceneManager.runningScene} || Previous Scene : {SceneManager.previousScene} || Next Scene : {SceneManager.nextScene}", 1, 15, 10, Color.White);


            Raylib.EndDrawing();
            
        if (closeGame) Raylib.CloseWindow();
        }
        //Raylib.CloseWindow();
        return Task.CompletedTask;
    }
}