using UnityEngine;

namespace Selivura
{
    public class Timer
    {
        public float TimeLeft;
        private float _startTime;
        private float _timerDuration;
        public bool Expired => Time.time - _startTime >= _timerDuration;
        public Timer(float duration, float startTime)
        {
            _startTime = startTime;
            _timerDuration = duration;
        }
    }
}
