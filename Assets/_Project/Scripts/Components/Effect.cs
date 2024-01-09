using UnityEngine;

namespace Selivura
{
    public class Effect : MonoBehaviour
    {
        [SerializeField] float _lifetime = 1;
        [SerializeField] bool _autoDespawn;
        private void OnEnable()
        {
            if (_autoDespawn)
            {
                Invoke(nameof(Despawn), _lifetime);
            }
        }
        public void Despawn()
        {
            gameObject.SetActive(false);
        }
    }
}
