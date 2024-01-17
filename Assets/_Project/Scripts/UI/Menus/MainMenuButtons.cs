using UnityEngine;
using UnityEngine.SceneManagement;

namespace Selivura.UI
{
    public class MainMenuButtons : MonoBehaviour
    {
        public void PlayButton()
        {
            SceneManager.LoadSceneAsync(1);
        }
        public void QuitButton()
        {
            Application.Quit();
        }
    }
}
