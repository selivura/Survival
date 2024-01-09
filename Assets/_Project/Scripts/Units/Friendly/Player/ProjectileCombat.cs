using Selivura.ObjectPooling;
using UnityEngine;

namespace Selivura.Player
{
    public class ProjectileCombat : Combat
    {
        [SerializeField] Transform _projectilePoolContainer;
        [SerializeField] Projectile _projectilePrefab;
        PoolingSystem<Projectile> _pool;
        Timer _attackCooldownTimer = new Timer(0, 0);
        public bool CanAttack { get { return _attackCooldownTimer.Expired; } }
        private void Awake()
        {
            _pool = new PoolingSystem<Projectile>(_projectilePoolContainer);
        }
        public override void Attack(Vector2 direction, AttackData data)
        {
            if (!CanAttack)
                return;

            var projectileData = new ProjectileData.Builder()
                .WithDamage(data.Damage)
                .WithSpeed(data.ProjectileSpeed)
                .WithLifetime(data.AttackRange / data.ProjectileSpeed)
                .Build();

            _attackCooldownTimer = new Timer(data.AttackCooldown, Time.time);

            var spawned = _pool.Get(_projectilePrefab);
            spawned.Initialize(projectileData, direction);
            spawned.transform.position = transform.position;
            OnAttack?.Invoke();
        }
    }
}
