using Selivura.ObjectPooling;
using UnityEngine;

namespace Selivura.Player
{
    public class ProjectileCombat : Combat
    {
        public Transform ProjectilePoolContainer;
        public Projectile ProjectilePrefab;
        PoolingSystem<Projectile> _pool;
        Timer _attackCooldownTimer = new Timer(0, 0);
        public bool CanAttack { get { return _attackCooldownTimer.Expired; } }
        private void Awake()
        {
            _pool = new PoolingSystem<Projectile>(ProjectilePoolContainer);
        }
        public override void Attack(Vector2 direction, AttackData data)
        {
            if (!CanAttack)
                return;

            var projectileData = new ProjectileData.Builder()
                .WithDamage(data.Damage)
                .WithSpeed(data.ProjectileSpeed)
                .WithLifetime(data.AttackRange / data.ProjectileSpeed)
                .WithHealth(data.ProjectileHealth)
                .Build();

            _attackCooldownTimer = new Timer(data.AttackCooldown, Time.time);

            var spawned = _pool.Get(ProjectilePrefab);
            spawned.Initialize(projectileData, direction);
            spawned.transform.position = transform.position;
            OnAttack?.Invoke();
        }
    }
}
