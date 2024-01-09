using Selivura.Player;
using UnityEngine;

namespace Selivura
{
    public class PlayerHandsAnimation : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] PlayerController _controller;
        [SerializeField] PlayerUnit _playerUnit;
        [SerializeField] Combat _combat;
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] string _attackStateName = "Shoot";
        [SerializeField] int _orderInLayerDefault = 150;
        [SerializeField] int _orderInLayerWalkingUp = 50;
        private void OnEnable()
        {
            _combat.OnAttack.AddListener(PlayShootAnimation);
        }
        private void OnDisable()
        {
            _combat.OnAttack.RemoveListener(PlayShootAnimation);
        }
        private void PlayShootAnimation()
        {
            _animator.Play(_attackStateName);
        }

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
            if (_controller.DirectionInput.x < 0)
                _spriteRenderer.flipY = true;
            else
                _spriteRenderer.flipY = false;
            transform.right = _controller.DirectionInput;
            if (_controller.MovementInput.y > 0)
            {
                _spriteRenderer.sortingOrder = _orderInLayerWalkingUp;
            }
            else
            {
                _spriteRenderer.sortingOrder = _orderInLayerDefault;
            }
        }

    }
}
