using UnityEngine;

namespace Selivura.UI
{
    public class InteractIndicator : TextDisplayUI
    {
        [SerializeField] private GameObject _interactIndicator;
        [SerializeField] private GameObject _lockedinteractIndicator;
        private void Awake()
        {
            _interactIndicator.SetActive(false);
            _lockedinteractIndicator.SetActive(false);
        }
        public void ShowInteractibleIndicator(string name)
        {
            if(name != "")
            {
                tmpText.text = prefix + name;
            }
            else
            {
                return;
            }
            _interactIndicator.SetActive(true);
            _lockedinteractIndicator.SetActive(false);
        }
        public void ShowLockedIndicator()
        {
            _interactIndicator.SetActive(false);
            _lockedinteractIndicator.SetActive(true);
        }
        public void HideAllIndicators()
        {
            _interactIndicator.SetActive(false);
            _lockedinteractIndicator.SetActive(false);
        }
    }
}
