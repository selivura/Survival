using Selivura.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Selivura
{
    public class GameStateController : MonoBehaviour, IDependecyProvider
    {
        public UnityEvent OnGameOver;

        [Provide]
        protected GameStateController Provide()
        {
            return this;
        }
        public void EndGame()
        {
            OnGameOver?.Invoke();
        }
        public void GoToMainMenu()
        {
            EndGame();
            SceneManager.LoadSceneAsync(0);
        }
        public static void RestartGame()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
