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
        public static Music gameMusic;
        public static Sound getAmmo;
        public static Sound grow;
        public static Sound hedgehogKilled;
        public static Sound HedgehogHit;
        public static Sound clic;
        public static Sound wallDestroyed;
        public static Sound wallHit;
        public static Sound shoot;
        public static Sound gameOver;
        public static Sound victory;
        static float timePlayed = Raylib.GetMusicTimePlayed(gameMusic) / Raylib.GetMusicTimeLength(gameMusic);

        public static void LoadMusic()
        {
            gameMusic = Raylib.LoadMusicStream("audio/loopsong.mp3");
            getAmmo = Raylib.LoadSound("audio/ammo.wav");
            grow = Raylib.LoadSound("audio/grow.wav");
            gameOver = Raylib.LoadSound("audio/gameover.wav");
            hedgehogKilled = Raylib.LoadSound("audio/hedgehogkilled.wav");
            HedgehogHit = Raylib.LoadSound("audio/hitinghedgehog.wav");
            clic = Raylib.LoadSound("audio/clic.wav");
            wallDestroyed = Raylib.LoadSound("audio/walldestroyed.wav");
            wallHit = Raylib.LoadSound("audio/hitingwalls.wav");
            shoot = Raylib.LoadSound("audio/shoot.wav");
            victory = Raylib.LoadSound("audio/victory.wav");
        }

        public static void PlayMusic(Music music)
        {
            Raylib.PlayMusicStream(music);
        }

        public static void PlaySound(Sound music)
        {
            Raylib.PlaySound(music);
        }

        public static void UpdateMusic(Music music)
        {
            Raylib.UpdateMusicStream(music);
            if (timePlayed > 1.0f)
            {
                timePlayed = 1f;
                Raylib.PlayMusicStream(music);
            }
        }

        public static void StopMusic(Music music)
        {
            Raylib.StopMusicStream(music);
        }

    }


}
