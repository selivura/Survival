using UnityEngine;

namespace Selivura
{
    [System.Serializable]
    public class Timer
    {
        public float TimeLeft => _timerDuration - (Time.time - _startTime);
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
