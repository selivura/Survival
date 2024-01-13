using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Selivura.UI
{
    public class AttentionMessage : MonoBehaviour, IDependecyProvider
    {
        [SerializeField] GameObject _messageContainer;
        [SerializeField] TMP_Text _messageText;
        [SerializeField] float _messageShowAnimationDuration = 0.5f;
        [SerializeField] float _messageDuration = 2f;
        [SerializeField] float _messageHideDuration = 0.5f;
        [Provide]
        public AttentionMessage Provide()
        {
            return this;
        }
        private void Start()
        {
            _messageContainer.transform.localScale.Set(1, 0, 1);
        }
        public void ShowMessage(string message)
        {
            CancelInvoke(nameof(HideMessage));

            DOTween.Complete(_messageContainer.transform);
            _messageContainer.transform.localScale.Set(1, 0, 1);
            _messageContainer.transform.DOScaleY(1, _messageShowAnimationDuration);
            Invoke(nameof(HideMessage), _messageDuration);

            _messageText.text = message;
        }
        private void HideMessage()
        {
            _messageContainer.transform.DOScaleY(0, _messageHideDuration);
        }
    }
}
