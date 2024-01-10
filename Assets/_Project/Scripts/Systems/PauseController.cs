using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public class PauseController : MonoBehaviour, IDependecyProvider
    {
        public bool IsGamePaused;
        public UnityEvent OnPause;
        public UnityEvent OnUnpause;

        [Provide]
        public PauseController Provide()
        {
            return this;
        }

        public void PauseGame(bool pause)
        {
            IsGamePaused = pause;
            Time.timeScale = IsGamePaused ? 0 : 1;
        }
    }
}
