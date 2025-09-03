using Raylib_cs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Code
{
    public class SceneCommands : Scene
    {
        public Methodes methodes = new Methodes();
        public static Button closeCommandsButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 80, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "Ok", 30, Color.Maroon, Color.LightGray);

        Texture2D upArrow = Raylib.LoadTexture("images/COMMAND_UP.png");
        Texture2D downArrow = Raylib.LoadTexture("images/COMMAND_DOWN.png");
        Texture2D leftArrow = Raylib.LoadTexture("images/COMMAND_L.png");
        Texture2D rightArrow = Raylib.LoadTexture("images/COMMAND_R.png");
        Texture2D spaceBar = Raylib.LoadTexture("images/COMMAND_SPACE.png");
        Texture2D qKey = Raylib.LoadTexture("images/COMMAND_Q.png");

        public override void Load()
        {
            SceneManager.runningScene = SceneManager.enumScene.GameOver;
            closeCommandsButton.isVisible = true;

        }

        public override void Update(float deltatime)
        {
            closeCommandsButton.MouseHover(closeCommandsButton);
            CloseCommandsEvent();

        }
        public override void Draw()
        {
            int firstY = 200;
            int spaceXY = 70;
            int spaceText = 40;
            int secondY = 400;

            Vector2 vTop = new Vector2((int)(Program.ScreenW * 0.5f + 180), secondY + spaceText * 3 + 5);
            Vector2 vLeft = new Vector2((int)(Program.ScreenW * 0.5f + 180 - 12), secondY + spaceText * 3 + 29);
            Vector2 vRight = new Vector2((int)(Program.ScreenW * 0.5f + 180 + 12), secondY + spaceText * 3 + 29);


            Raylib.ClearBackground(Color.LightGray);
            methodes.DrawCenteredText("COMMANDS", 100, 50, 4, Raylib.GetFontDefault(), Color.Maroon);

            Raylib.DrawTexture(upArrow, 20 + spaceXY * 0, firstY + spaceXY * 0, Color.White);
            Raylib.DrawTexture(downArrow, 20 + spaceXY * 1, firstY + spaceXY * 0, Color.White);
            Raylib.DrawTexture(leftArrow, 20 + spaceXY * 2, firstY + spaceXY * 0, Color.White);
            Raylib.DrawTexture(rightArrow, 20 + spaceXY * 3, firstY + spaceXY * 0, Color.White);
            Raylib.DrawText("Move snake", (int)(20 + spaceXY * 4.5f), firstY + 20, 30, Color.Maroon);

            Raylib.DrawTexture(spaceBar, 20 + spaceXY * 0 + 20, firstY + spaceXY * 1, Color.White);
            Raylib.DrawText("Shot", 20 + spaceXY * 2 + 20, firstY + spaceXY * 1 + 20, 30, Color.Maroon);

            Raylib.DrawTexture(qKey, 20 + spaceXY * 0, firstY + spaceXY * 2, Color.White);
            Raylib.DrawText("Pause (QWERTY)", 20 + spaceXY * 1 + 20, firstY + spaceXY * 2 + 20, 30, Color.Maroon);

            methodes.DrawCenteredText("HOW TO PLAY ?", secondY, 50, 4, Raylib.GetFontDefault(), Color.Maroon);
            methodes.DrawCenteredText("Eat apples to grow", secondY + spaceText * 2, 30, 4, Raylib.GetFontDefault(), Color.Maroon);
            Raylib.DrawCircle((int)(Program.ScreenW * 0.5f + 180), secondY + spaceText * 2 + 15, Grid.CELLH * 0.5f, Color.Red);

            methodes.DrawCenteredText("Avoid hedgehogs", secondY + spaceText * 3, 30, 4, Raylib.GetFontDefault(), Color.Maroon);
            methodes.DrawCenteredText("(Be carefull, they spawn regularly)", secondY + spaceText * 4, 30, 4, Raylib.GetFontDefault(), Color.Maroon);
            Raylib.DrawTriangle(vTop, vLeft, vRight, Color.Brown);


            methodes.DrawCenteredText("Shot bullets", secondY + spaceText * 5, 30, 4, Raylib.GetFontDefault(), Color.Maroon);
            Raylib.DrawRectangle((int)(Program.ScreenW * 0.5f + 170), secondY + spaceText * 5 + 5, Grid.CELLW, Grid.CELLH, Color.Black);

            methodes.DrawCenteredText("And destroy walls !", secondY + spaceText * 6, 30, 4, Raylib.GetFontDefault(), Color.Maroon);
            Raylib.DrawRectangle((int)(Program.ScreenW * 0.5f + 170), secondY + spaceText * 6 + 5, Grid.CELLW, Grid.CELLH, Color.Gray);


            closeCommandsButton.ButtonDraw();

        }

        public override void Unload()
        {
            closeCommandsButton.isVisible = false;
            SceneManager.previousScene = SceneManager.runningScene;

            //Raylib.UnloadTexture(upArrow);
            //Raylib.UnloadTexture(downArrow);
            //Raylib.UnloadTexture(leftArrow);
            //Raylib.UnloadTexture(rightArrow);
            //Raylib.UnloadTexture(spaceBar);
            //Raylib.UnloadTexture(qKey);

        }

        public void CloseCommandsEvent()
        {
            if (closeCommandsButton.isVisible && closeCommandsButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                if (SceneManager.previousScene == SceneManager.enumScene.Menu)
                {
                    SceneManager.Load<SceneMenu>();
                }
                else if (SceneManager.previousScene == SceneManager.enumScene.Pause)
                {
                    SceneManager.Load<ScenePauseGame>();

                }

            }
        }
    }
}
