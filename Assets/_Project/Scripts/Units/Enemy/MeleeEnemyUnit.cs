using UnityEngine;

namespace Selivura
{
    public class MeleeEnemyUnit : EnemyUnit
    {
        [SerializeField] int _attackDamage = 25;
        [SerializeField] float _speed = 1;
        [SerializeField] float _maxAggroDistance = 25;
        [SerializeField] float _attackDistance = 2;
        [SerializeField] LayerMask _targetLayerMask;
        [SerializeField] float _attackCooldown = 1;
        [SerializeField] float _targetSearchCooldown = 1;
        float _lastAttackTime;
        float _lastSearchTime;

        public bool CanSearchForTarget { get { return Time.time - _lastSearchTime > _targetSearchCooldown; } }
        public bool CanAttack { get { return Time.time - _lastAttackTime > _attackCooldown; } }
        FriendlyUnit _target;
        IMoveable _movement;
        private void Awake()
        {
            _movement = GetComponent<IMoveable>();
            SetTarget(FindAnyObjectByType<MainBase>());
        }
        private void FixedUpdate()
        {
            if (_target != null)
            {
                float targetDistance = Vector2.Distance(_target.transform.position, transform.position);
                if (targetDistance < _attackDistance)
                {
                    if (CanAttack)
                    {
                        AttackTarget(_target);
                    }
                }
                else
                    _movement.Move(transform.position.NormalizedDirectionTo(_target.transform.position), _speed);
            }
            SearchForTarget();
        }

        private void AttackTarget(Unit target)
        {
            target.ChangeHealth(-_attackDamage);
            _lastAttackTime = Time.time;
        }

        public void SetTarget(FriendlyUnit target)
        {
            _target = target;
        }
        public void SearchForTarget()
        {
            if (!CanSearchForTarget)
                return;
            var foundTargets = Physics2D.OverlapCircleAll(transform.position, _maxAggroDistance, _targetLayerMask);
            _lastSearchTime = Time.time;
            if (foundTargets.Length <= 0)
            {
                SetTarget(null);
                return;
            }
            float nearestDistance = float.PositiveInfinity;
            Collider2D nearestTarget = foundTargets[0];
            foreach (var foundTarget in foundTargets)
            {
                float distance = Vector2.Distance(foundTarget.transform.position, transform.position);
                if (distance < nearestDistance)
                {
                    nearestTarget = foundTarget;
                    nearestDistance = distance;
                }
            }
            if (nearestTarget.TryGetComponent(out FriendlyUnit target))
                SetTarget(target);
            else
                SetTarget(null);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _maxAggroDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackDistance);
        }
    }
}
