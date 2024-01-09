using UnityEngine;

namespace Selivura
{
    public interface IMoveable
    {
        public Vector2 MovementVelocity { get; }
        void Move(Vector2 direction, float speed);
    }
}