using Selivura.Player;
using UnityEngine;

namespace Selivura
{
    public class MatterCollectible : MonoBehaviour
    {
        [SerializeField] int _amount = 1;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerUnit playerUnit))
            {
                playerUnit.ChangeMatter(_amount);
                gameObject.SetActive(false);
            }
        }
    }
}
