using UnityEngine;

namespace Selivura
{
    public class Effect : MonoBehaviour
    {
        [SerializeField] float _lifetime = 1;
        [SerializeField] bool _autoDespawn;
        Timer _timer = new Timer(0,0);
        public void Setup(float lifetime, bool autoDespawn)
        {
            _lifetime = lifetime;
            _autoDespawn = autoDespawn;
            _timer = new Timer(_lifetime, Time.time);
        }
        private void OnEnable()
        {
            if (_autoDespawn)
            {
                _timer = new Timer(_lifetime, Time.time);
            }
        }
        private void FixedUpdate()
        {
            if(_timer.Expired && _autoDespawn)
            {
                Despawn();
            }
        }
        public void Despawn()
        {
            gameObject.SetActive(false);
        }
    }
}
