namespace Selivura
{
    public class AttackData
    {
        public int Damage = 5;
        public float AttackCooldown = 0.25f;
        public float ProjectileSpeed = 3;
        public float AttackRange = 5;
        public int ProjectileHealth = 2;
        public AttackData(int damage, float attackCooldown, float projectileSpeed, float attackRange, int projectileHealth)
        {
            Damage = damage;
            AttackCooldown = attackCooldown;
            ProjectileSpeed = projectileSpeed;
            AttackRange = attackRange;
            ProjectileHealth = projectileHealth;
        }
    }
}
