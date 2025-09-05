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
        Color textColor = Program.greenLemon;
        Color backgroundColor = Program.darkGreen;
        public Methodes methodes = new Methodes();
        public static Button closeCommandsButton = new Button((int)(Program.ScreenW * 0.5f - 110), Program.ScreenH - 80, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "Ok", 30, Color.Maroon, Color.LightGray);

        public override void Load()
        {
            SceneManager.runningScene = SceneManager.enumScene.Commands;
            closeCommandsButton.isVisible = true;
        }

        public override void Update(float deltatime)
        {
            closeCommandsButton.MouseHover(closeCommandsButton);
            CloseCommandsEvent();
        }
        public override void Draw()
        {
            int firstY = 120;
            int spaceXY = 70;
            int spaceText = 40;
            int secondY = 400;

            Raylib.ClearBackground(backgroundColor);
            methodes.DrawCenteredText("COMMANDS", 50, 50, 4, Raylib.GetFontDefault(), textColor);

            Raylib.DrawTexture(TextureManager.upArrow, 20 + spaceXY * 0, firstY + spaceXY * 0, Color.White);
            Raylib.DrawTexture(TextureManager.downArrow, 20 + spaceXY * 1, firstY + spaceXY * 0, Color.White);
            Raylib.DrawTexture(TextureManager.leftArrow, 20 + spaceXY * 2, firstY + spaceXY * 0, Color.White);
            Raylib.DrawTexture(TextureManager.rightArrow, 20 + spaceXY * 3, firstY + spaceXY * 0, Color.White);
            Raylib.DrawText("Move snake", (int)(20 + spaceXY * 4.5f), firstY + 20, 30, textColor);

            Raylib.DrawTexture(TextureManager.spaceBar, 20 + spaceXY * 0 + 20, firstY + spaceXY * 1, Color.White);
            Raylib.DrawText("Shot Venom", 20 + spaceXY * 2 + 20, firstY + spaceXY * 1 + 20, 30, textColor);

            Raylib.DrawTexture(TextureManager.qKey, 20 + spaceXY * 0, firstY + spaceXY * 2, Color.White);
            Raylib.DrawText("Pause (QWERTY)", 20 + spaceXY * 1 + 20, firstY + spaceXY * 2 + 20, 30, textColor);

            methodes.DrawCenteredText("HOW TO PLAY ?", secondY, 50, 4, Raylib.GetFontDefault(), textColor);
            methodes.DrawCenteredText("Eat apples to grow", secondY + spaceText * 2, 30, 4, Raylib.GetFontDefault(), textColor);
            Raylib.DrawTexture(TextureManager.apple, (int)(Program.ScreenW * 0.5f + 180), secondY + spaceText * 2 + 5, Color.White);

            methodes.DrawCenteredText("Collect poison, spit venom", secondY + spaceText * 3, 30, 4, Raylib.GetFontDefault(), textColor);
            Raylib.DrawTexture(TextureManager.poison, (int)(Program.ScreenW * 0.5f + 250), secondY + spaceText * 3 + 5, Color.White);
            Raylib.DrawTexture(TextureManager.venom, (int)(Program.ScreenW * 0.5f + 280), secondY + spaceText * 3 + 5, Color.White);

            methodes.DrawCenteredText("Kill hedghogs", secondY + spaceText * 4, 30, 4, Raylib.GetFontDefault(), textColor);
            methodes.DrawCenteredText("(Be carefull, they spawn regularly)", secondY + spaceText * 5, 30, 4, Raylib.GetFontDefault(), textColor);
            Raylib.DrawTexture(TextureManager.hedgehog, (int)(Program.ScreenW * 0.5f + 170), secondY + spaceText * 4 + 5, Color.White);

            methodes.DrawCenteredText("And destroy walls !", secondY + spaceText * 6, 30, 4, Raylib.GetFontDefault(), textColor);
            Raylib.DrawTexture(TextureManager.wall, (int)(Program.ScreenW * 0.5f + 170), secondY + spaceText * 6 + 5, Color.White);

            closeCommandsButton.ButtonDraw();
        }

        public override void Unload()
        {
            closeCommandsButton.isVisible = false;
            SceneManager.previousScene = SceneManager.runningScene;
        }

        public void CloseCommandsEvent()
        {
            if (closeCommandsButton.isVisible && closeCommandsButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                closeCommandsButton.ButtonClic();

                if (SceneManager.previousScene == SceneManager.enumScene.Menu)
                {
                    if (SceneManager.nextScene == SceneManager.enumScene.Game)
                    {
                        SceneManager.Load<SceneGame>();
                    }
                    else
                    {
                        SceneManager.Load<SceneMenu>();
                    }
                }
                if (SceneManager.previousScene == SceneManager.enumScene.Pause)
                {
                    SceneManager.Load<ScenePauseGame>();
                }


            }
        }
    }
}
