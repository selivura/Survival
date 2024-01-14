using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DroneMovement : MonoBehaviour, IMovement
    {
        protected new Rigidbody2D rigidbody;
       [SerializeField] private float _stoppedSpeed = 0.25f;
        public Vector2 MovementVelocity { get => rigidbody.velocity; }
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }
        public void Move(Vector2 direction, float speed)
        {
            direction.Normalize();
            transform.right = direction;
            rigidbody.velocity = transform.right * speed;
        }
        public void Stop()
        {
            rigidbody.velocity =
            new Vector2(
                Mathf.Clamp(rigidbody.velocity.x, -_stoppedSpeed, _stoppedSpeed),
            Mathf.Clamp(rigidbody.velocity.y, -_stoppedSpeed, _stoppedSpeed));
        }
    }
}
