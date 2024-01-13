using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidbodyMovement : MonoBehaviour, IMovement
    {
        Rigidbody2D _rigidbody;
        public Vector2 MovementVelocity { get => _rigidbody.velocity; }
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        public void Move(Vector2 direction, float speed)
        {
            direction.Normalize();
            _rigidbody.velocity = (Vector3)direction * speed;
        }
        public void Stop()
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
