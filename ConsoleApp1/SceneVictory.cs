using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Code
{
    public class SceneVictory : Scene
    {
        public Methodes methodes = new Methodes();

        public static List<Tuple<int, string>> HighScores = new List<Tuple<int, string>>();

        int MouseX;
        int MouseY;

        int MAX_CHAR_NAME = 10;

        public static Button EnterNamePanel = new Button((int)(Program.ScreenW * 0.5f - 150), (int)(Program.ScreenH * 0.5f), 300, 50, Color.Red, Color.Gray, Color.LightGray, "", 30, Color.Black, Color.Red);
        public static Button EnterNameButton = new Button((int)(Program.ScreenW * 0.5f - 110), (int)(Program.ScreenH * 0.5f) + 250, 220, 50, Color.White, Color.Black, Color.LightGray, "OK", 30, Color.White, Color.Black);

        char[] name;
        string nameString;

        int letterCount = 0;

        int frameCounter = 0;

        int nameX = (int)(Program.ScreenW * 0.5f - 150) + 5;
        int nameY = (int)(Program.ScreenH * 0.5f);

        int finalScore;

        public override void Load()
        {
            SceneManager.runningScene = SceneManager.enumScene.Victory;
            
            if (ScoreManager.score < 0)
            {
                finalScore = 0;
            }
            else
            {
                finalScore = ScoreManager.score;
            }

            EnterNameButton.isVisible = true;
            EnterNamePanel.isVisible = true;
            name = new char[MAX_CHAR_NAME + 1];
        }

        public override void Update(float deltatime)
        {
            EnterNameButton.MouseHover(EnterNameButton);
            EnterNamePanel.MouseHover(EnterNamePanel);
            ValidateNameEvent();

            if (EnterNamePanel.isHover)
            {
                Raylib.SetMouseCursor(MouseCursor.IBeam);

                int key = Raylib.GetCharPressed();

                while (key > 0)
                {
                    if (key >= 32 && key <= 125 && letterCount < MAX_CHAR_NAME)
                    {
                        name[letterCount] = (char)key;
                        name[letterCount + 1] = '\0';
                        letterCount++;
                        Console.WriteLine(name);
                    }

                    key = Raylib.GetCharPressed();
                }

                if (Raylib.IsKeyPressed(KeyboardKey.Backspace))
                {
                    letterCount--;
                    if (letterCount < 0) letterCount = 0;
                    name[letterCount] = '\0';
                    Console.WriteLine(name);
                }

                nameString = new string(name);

            }


            else Raylib.SetMouseCursor(MouseCursor.Default);

            if (EnterNamePanel.isHover) frameCounter++;
            else frameCounter = 0;

        }
        public override void Draw()
        {
            Raylib.ClearBackground(Color.Black);

            methodes.DrawCenteredText("VICTORY !!", 100, 50, 4, Raylib.GetFontDefault(), Color.White);
            methodes.DrawCenteredText($"Score : {finalScore} pts", 180, 45, 4, Raylib.GetFontDefault(), Color.White);


            methodes.DrawCenteredText("Enter your name", nameY - 60, 50, 4, Raylib.GetFontDefault(), Color.White);
            EnterNameButton.ButtonDraw();
            EnterNamePanel.ButtonDraw();

            Raylib.DrawText(nameString, nameX, nameY + 15, 30, Color.Maroon);
            Raylib.DrawText($"{letterCount}/{MAX_CHAR_NAME}", nameX, nameY + 60, 20, Color.White);

            if (EnterNamePanel.isHover)
            {
                if (letterCount < MAX_CHAR_NAME)
                {
                    if ((frameCounter / 20) % 2 == 0) Raylib.DrawText("_", nameX + 4 + Raylib.MeasureText(nameString, 30), nameY + 15, 30, Color.Maroon);
                }
            }

        }

        public override void Unload()
        {
            EnterNameButton.isVisible = false;
            EnterNamePanel.isVisible = false;
        }

        public void ValidateNameEvent()
        {
            if (EnterNameButton.isVisible && EnterNameButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                string subName = nameString.Substring(0, letterCount);

                HighScores.Add(new Tuple<int, string>(finalScore, subName));
                HighScores.Add(new Tuple<int, string>(500, "Kaa"));
                HighScores.Add(new Tuple<int, string>(800, "Nagini"));
                HighScores.Add(new Tuple<int, string>(450, "Thulsa Doom"));

                SceneManager.Load<SceneHighScores>();
            }
        }

    }
}
