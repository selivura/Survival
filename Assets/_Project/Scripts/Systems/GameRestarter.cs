using UnityEngine;
using UnityEngine.SceneManagement;

namespace Selivura
{
    public class GameRestarter : MonoBehaviour
    {
        [SerializeField] FriendlyBase _base;
        [SerializeField] Unit _player;
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
