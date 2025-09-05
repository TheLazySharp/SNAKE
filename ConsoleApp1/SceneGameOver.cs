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
    public class SceneGameOver : Scene
    {
        public Methodes methodes = new Methodes();

        int MAX_CHAR_NAME = 10;
        char[] name;
        string nameString;

        public static Button EnterNamePanel = new Button((int)(Program.ScreenW * 0.5f - 150), (int)(Program.ScreenH * 0.5f), 300, 50, Color.Maroon, Color.LightGray, Color.LightGray, "", 30, Color.Maroon, Color.Maroon);
        public static Button EnterNameButton = new Button((int)(Program.ScreenW * 0.5f - 110), (int)(Program.ScreenH * 0.5f) + 250, 220, 50, Color.Maroon, Color.LightGray, Color.Maroon, "Ok", 30, Color.Maroon, Color.LightGray);


        int letterCount = 0;

        int frameCounter = 0;

        int nameX = (int)(Program.ScreenW * 0.5f - 150) + 5;
        int nameY = (int)(Program.ScreenH * 0.5f);

        int finalScore;

        Color textColor = Program.greenLemon;
        Color backgroundColor = Program.darkGreen;

        public override void Load()
        {
            SceneManager.runningScene = SceneManager.enumScene.GameOver;
            AudioManager.PlaySound(AudioManager.gameOver);


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
            Raylib.ClearBackground(backgroundColor);

            methodes.DrawCenteredText("GAME OVER !!", 100, 50, 4, Raylib.GetFontDefault(), textColor);
            methodes.DrawCenteredText($"Score : {finalScore} pts", 180, 45, 4, Raylib.GetFontDefault(), textColor);


            methodes.DrawCenteredText("Enter your name", nameY - 60, 50, 4, Raylib.GetFontDefault(), textColor);
            EnterNameButton.ButtonDraw();
            EnterNamePanel.ButtonDraw();

            if (EnterNamePanel.isHover) Raylib.DrawText(nameString, nameX, nameY + 15, 30, backgroundColor);
            else Raylib.DrawText(nameString, nameX, nameY + 15, 30, textColor);
            Raylib.DrawText($"{letterCount}/{MAX_CHAR_NAME} (min 4 char)", nameX, nameY + 60, 20, textColor);

            if (EnterNamePanel.isHover)
            {
                if (letterCount < MAX_CHAR_NAME)
                {
                    if ((frameCounter / 20) % 2 == 0) Raylib.DrawText("_", nameX + 4 + Raylib.MeasureText(nameString, 30), nameY + 15, 30, textColor);
                }
            }
        }

        public override void Unload()
        {
            EnterNameButton.isVisible = false;
            EnterNamePanel.isVisible = false;
            SceneManager.previousScene = SceneManager.runningScene;
        }

        public void ValidateNameEvent()
        {
            if (letterCount > 3 && EnterNameButton.isVisible && EnterNameButton.isHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                EnterNameButton.ButtonClic();

                string subName = nameString.Substring(0, letterCount);
                ScoreManager.HighScores.Add(new Tuple<int, string>(finalScore, subName));

                if (Program.nbGames == 1)
                {
                    ScoreManager.HighScores.Add(new Tuple<int, string>(2500, "Kaa"));
                    ScoreManager.HighScores.Add(new Tuple<int, string>(10000, "Nagini"));
                    ScoreManager.HighScores.Add(new Tuple<int, string>(1000, "Thulsa Doom"));
                }

                SceneManager.Load<SceneHighScores>();
            }
        }

    }
}
