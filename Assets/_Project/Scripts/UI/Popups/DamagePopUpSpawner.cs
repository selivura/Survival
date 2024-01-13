using UnityEngine;

namespace Selivura.UI
{
    public class DamagePopUpSpawner : MonoBehaviour
    {
        [SerializeField] DamagePopUpFactory _damagePopUpFactory;
        public void SpawnDamagePopUp(int damageAmount)
        {
            _damagePopUpFactory.CreateAndSetPosition(transform.position)
                .Setup(damageAmount);
        }
    }
}
