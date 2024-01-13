using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Selivura.UI
{
    public class GameResultScreen : MonoBehaviour
    {
        [SerializeField] GameObject Container;

        [Inject]
        PauseController _pauseController;

        [SerializeField] PauseMenuUI _pauseMenu;

        [Inject]
        GameStateController _gameStateController;
        private void Awake()
        {
             FindFirstObjectByType<Injector>().Inject(this);
            Show(false);
        }
        public void Show(bool show)
        {
            Container.SetActive(show);
            if(_pauseMenu != null)
            {
                _pauseMenu.CanBeOpened = !show;
            }
            _pauseController.PauseGame(show);
        }
        public void RestartButton()
        {
            GameStateController.RestartGame();
            Show(false);
        }
        public void MainMenuButton()
        {
            _gameStateController.GoToMainMenu();
        }
    }
}
