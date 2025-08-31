using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class Timer
    {
        private float elapsedTime = 0f;
        public float duration { get; private set; }
        public bool isLooping;
        public bool isRunning { get; private set; }
        public Action? Callback { private get; set; }

        public Timer(float duration, bool isLooping, Action? callback = null)
        {
            this.duration = duration;
            this.isLooping = isLooping;
            this.Callback = callback;
            elapsedTime = 0f;
            isRunning = true;
        }

        public void UpdateTimerAtEnd()
        {
            if (!isRunning) return;
            elapsedTime += Raylib.GetFrameTime();
            //Console.WriteLine(elapsedTime);

            if (elapsedTime >= duration)
            {
                //Console.WriteLine(elapsedTime);

                Callback?.Invoke();
                if (isLooping)
                    elapsedTime = 0f;
                else StopTimer();
            }
        }

        public void UpdateDuringTimer()
        {
            if (!isRunning) return;
            elapsedTime += Raylib.GetFrameTime();
            //Console.WriteLine(elapsedTime);

            if (elapsedTime < duration)
            {
                //Console.WriteLine(elapsedTime);

                Callback?.Invoke();

            }
            else
            {
                if (isLooping)
                    elapsedTime = 0f;
                else StopTimer();
            }
        }



        public void StopTimer()
        {
            isRunning = false;
        }

        public void StartTimer()
        {
            elapsedTime = 0f;
            isRunning = true;
        }

        public void PauseTimer()
        {
            isRunning = false;
        }

        public void ResetTimer()
        {
            elapsedTime = 0f;
        }

        public void SetDuration(float newDuration)
        {
            duration = newDuration;
        }

        public bool isTimerFinished()
        {
            return elapsedTime >= duration;
        }
    }
}
