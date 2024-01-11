using UnityEngine;

namespace Selivura.Player
{
    public class PrimitiveCombat : Combat
    {
        [SerializeField] LayerMask _targetLayerMask;
        [SerializeField] float _attackCooldown = .1f;
        [SerializeField] float _attackDistance = 2;
        float _lastAttackTime;
        public bool CanAttack { get { return Time.time - _lastAttackTime > _attackCooldown; } }
        public override void Attack(Vector2 direction, AttackData data)
        {
            if (!CanAttack)
                return;
            _lastAttackTime = Time.time;
            var foundTargets = Physics2D.OverlapCircleAll(transform.position, _attackDistance, _targetLayerMask);
            foreach (var foundTarget in foundTargets)
            {
                if (foundTarget.TryGetComponent(out EnemyUnit targetEnemy))
                {
                    targetEnemy.ChangeHealth(-data.Damage);
                }
            }
        }
    }
}
