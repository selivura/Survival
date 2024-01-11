using Selivura.Player;
using UnityEngine;

namespace Selivura
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] PlayerController _controller;
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] string _movementXParameter = "MovementX";
        [SerializeField] string _movementYParameter = "MovementY";

        private void FixedUpdate()
        {
            if (!_animator)
            {
                Debug.LogError("No Animator assigned");
                return;
            }
            if (!_controller)
            {
                Debug.LogError("No PlayerController assigned");
                return;
            }
            _animator.SetFloat(_movementXParameter, _controller.MovementInput.x);
            _animator.SetFloat(_movementYParameter, _controller.MovementInput.y);

            _spriteRenderer.FlipSprite(_controller.MovementInput.x);
        }


    }
}
