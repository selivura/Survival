using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Selivura.Player;
using UnityEngine.Events;

namespace Selivura.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        [SerializeField] GameObject _menuContainer;
        public UnityEvent OnMenuOpened;
        public UnityEvent OnMenuClosed;
        [Inject]
        private PauseController _pauseController;
        private void Awake()
        {
            Injector.Instance.Inject(this);
        }
        public void OpenMenu(bool value)
        {
            _menuContainer.SetActive(value);
            _pauseController.PauseGame(value);
            if(value)
            {
                OnMenuOpened.Invoke();
            }
            else
            {
                OnMenuClosed.Invoke();
            }
        }
        public void QuitGameButton()
        {
            Application.Quit();
        }
        
        public void RestartGameButton()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex); //TODO Добавить подтверждение сюда
        }
    }
}
