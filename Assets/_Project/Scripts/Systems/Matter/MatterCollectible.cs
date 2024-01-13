using Selivura.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public class MatterCollectible : MonoBehaviour
    {
        [SerializeField] int _amount = 1;
        public UnityEvent OnPickup;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerUnit playerUnit))
            {
                playerUnit.ChangeMatter(_amount);
                gameObject.SetActive(false);
                OnPickup?.Invoke();
            }
        }
    }
}
