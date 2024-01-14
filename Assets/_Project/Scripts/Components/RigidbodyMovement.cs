using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidbodyMovement : MonoBehaviour, IMovement
    {
        protected new Rigidbody2D rigidbody;
        public Vector2 MovementVelocity { get => rigidbody.velocity; }
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }
        public void Move(Vector2 direction, float speed)
        {
            direction.Normalize();
            rigidbody.velocity = (Vector3)direction * speed;
        }
        public void Stop()
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
}
