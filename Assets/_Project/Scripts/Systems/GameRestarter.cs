using Selivura.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Selivura
{
    public class GameRestarter : MonoBehaviour
    {
        [Inject]
        MainBase _base;
        [Inject]
        PlayerUnit _player;

        [SerializeField] GameObject GameOverScreen;
        private void Awake()
        {
            _base.OnKilled.AddListener(OnKilled);
            _player.OnKilled.AddListener(OnKilled);
        }
        private void OnDestroy()
        {
            _base.OnKilled.RemoveListener(OnKilled);
            _player.OnKilled.RemoveListener(OnKilled);
        }
        private void OnKilled(Unit unit)
        {
            GameOverScreen.SetActive(true);
            Invoke(nameof(Restart), 5);
        }
        private void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}
