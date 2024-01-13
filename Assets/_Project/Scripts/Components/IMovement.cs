using UnityEngine;

namespace Selivura
{
    public interface IMovement
    {
        public Vector2 MovementVelocity { get; }
        void Move(Vector2 direction, float speed);
        void Stop();
    }
}