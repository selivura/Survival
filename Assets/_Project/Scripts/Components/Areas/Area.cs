using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Area : MonoBehaviour
    {
        protected CircleCollider2D circleCollider;

        public UnityEvent OnAreaChanged;
        private void Awake()
        {
            circleCollider = GetComponent<CircleCollider2D>();
        }
        public float Radius
        {
            get
            {
                if(!circleCollider)
                    circleCollider = GetComponent<CircleCollider2D>();
                return circleCollider.radius;
            }
            set
            {
                circleCollider.radius = value;
                OnAreaChanged?.Invoke();
            }
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {

        }

        protected virtual void OnTriggerStay2D(Collider2D collision)
        {

        }
        protected virtual void OnTriggerExit2D(Collider2D collision)
        {

        }
        protected virtual void FixedUpdate()
        {

        }
    }
}
