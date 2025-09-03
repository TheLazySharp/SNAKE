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

namespace Code
{
    public static class AudioManager
    {

        private static Music gameMusic = Raylib.LoadMusicStream("audio/loopsong.mp3");
        static float timePlayed = Raylib.GetMusicTimePlayed(gameMusic) / Raylib.GetMusicTimeLength(gameMusic);

        public static void LoadMusic()
        {
            Raylib.PlayMusicStream(gameMusic);

        }

        public static void  UpdateMusic()
        {
            Raylib.UpdateMusicStream(gameMusic);
            if (timePlayed > 1.0f)
            {
                timePlayed = 0f;
                Raylib.PlayMusicStream(gameMusic);
            }

        }

        public static void StopMusic()
        {
            Raylib.StopMusicStream(gameMusic);
        }

    }


}
