using UnityEngine;

namespace Selivura
{
    public class Effect : MonoBehaviour
    {
        protected Timer timer = new Timer(9999, 0);
        public void Setup(float lifetime)
        {
            timer = new Timer(lifetime, Time.time);
        }
        private void FixedUpdate()
        {
            if (timer.Expired)
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
