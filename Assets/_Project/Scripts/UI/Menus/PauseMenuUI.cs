using UnityEngine;
using UnityEngine.Events;

namespace Selivura.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        [SerializeField] GameObject _menuContainer;
        public bool CanBeOpened = true;
        public UnityEvent OnMenuOpened;
        public UnityEvent OnMenuClosed;
        [Inject]
        private PauseController _pauseController;

        [Inject]
        GameStateController _gameStateController;
        private void Awake()
        {
            FindFirstObjectByType<Injector>().Inject(this);
            OpenMenu(false);
        }
        public void OpenMenu(bool value)
        {
            _menuContainer.SetActive(value);
            _pauseController.PauseGame(value);
            if (value)
            {
                OnMenuOpened.Invoke();
            }
            else
            {
                OnMenuClosed.Invoke();
            }
        }
        public void MainMenuButton()
        {
            OpenMenu(false);
            _gameStateController.GoToMainMenu();
        }

        public void RestartGameButton()
        {
            GameStateController.RestartGame(); //TODO Добавить подтверждение сюда
            OpenMenu(false);
        }
    }
}
