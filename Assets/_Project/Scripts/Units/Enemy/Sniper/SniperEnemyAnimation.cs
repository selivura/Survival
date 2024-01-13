using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(IMovement))]
    public class SniperEnemyAnimation : MonoBehaviour
    {
        IMovement _movement;
        [SerializeField] Animator _animator;
        [SerializeField] string _movementSpeedParameter = "MoveSpeed";
        private void OnEnable()
        {
            _movement = GetComponent<IMovement>();
            if (_animator == null)
            {
                Debug.LogError("No animator assigned");
            }
        }
        private void FixedUpdate()
        {
            if (_animator == null) return;
            _animator.SetFloat(_movementSpeedParameter, _movement.MovementVelocity.magnitude);
        }
    }
}
