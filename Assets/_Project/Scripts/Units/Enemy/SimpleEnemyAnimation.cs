using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(IMoveable))]
    public class SimpleEnemyAnimation : MonoBehaviour
    {
        IMoveable _movement;
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] Animator _animator;
        [SerializeField] string _movementXParameter = "MovementX";
        [SerializeField] string _movementYParameter = "MovementY";
        private void OnEnable()
        {
            _movement = GetComponent<IMoveable>();
            if (_animator == null)
            {
                Debug.LogError("No animator assigned");
            }
        }
        private void FixedUpdate()
        {
            if (_animator == null) return;
            _animator.SetFloat(_movementXParameter, _movement.MovementVelocity.x);
            _animator.SetFloat(_movementYParameter, _movement.MovementVelocity.y);
            _spriteRenderer.FlipSprite(_movement.MovementVelocity.x);
        }
    }
}
