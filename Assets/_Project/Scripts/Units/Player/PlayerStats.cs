namespace Selivura.Player
{
    public class PlayerStats
    {
        public PlayerStat Energy = new PlayerStat();
        public PlayerStat EnergyRegeneration = new PlayerStat();
        public PlayerStat EnergyDecay = new PlayerStat();
        public PlayerStat MovementSpeed = new PlayerStat();
        public PlayerStat AttackDamage = new PlayerStat();
        public PlayerStat AttackCooldown = new PlayerStat();
        public PlayerStat ProjectileSpeed = new PlayerStat();
        public PlayerStat AttackRange = new PlayerStat();
        public PlayerStat Penetration = new PlayerStat();
        public PlayerStats(float energy, float energyRegen, float energyDecay, float movementSpeed, float attackDamage, float attackCooldown, float projectileSpeed, float range, int penetration)
        {
            Energy = new PlayerStat(energy);
            EnergyRegeneration = new PlayerStat(energyRegen);
            EnergyDecay = new PlayerStat(energyDecay);
            MovementSpeed = new PlayerStat(movementSpeed);
            AttackDamage = new PlayerStat(attackDamage);
            AttackCooldown = new PlayerStat(attackCooldown);
            ProjectileSpeed = new PlayerStat(projectileSpeed);
            AttackRange = new PlayerStat(range);
            Penetration = new PlayerStat(penetration);
        }
        public class Builder
        {
            float energy = 75;
            float energyRegen = 25;
            float energyDecay = 4;
            float movementSpeed = 5;
            int attackDamage = 10;
            float attackCooldown = 0.4f;
            float projectileSpeed = 15;
            float range = 5;
            int penetration = 2;
            public Builder WithEnergy(float energy)
            {
                this.energy = energy;
                return this;
            }
            public Builder WithEnergyRegen(float energyRegen)
            {
                this.energyRegen = energyRegen;
                return this;
            }
            public Builder WithEnergyDecay(float energyDecay)
            {
                this.energyDecay = energyDecay;
                return this;
            }
            public Builder WithMovementSpeed(float movementSpeed)
            {
                this.movementSpeed = movementSpeed;
                return this;
            }
            public Builder WithAttackDamage(int attackDamage)
            {
                this.attackDamage = attackDamage;
                    return this;
            }
            public Builder WithAttackCD(float cd)
            {
                this.attackCooldown = cd;
                return this;
            }
            public Builder WithProjectileSpeed(float speed)
            {
                this.projectileSpeed = speed;
                return this;
            }
            public Builder WithRange(float range)
            {
                this.range = range;
                return this;
            }
            public Builder WithPenetration(int penetration)
            {
                this.penetration = penetration;
                return this;
            }
            public PlayerStats Build()
            {
                return new PlayerStats(energy, energyRegen, energyDecay, movementSpeed, attackDamage, attackCooldown, projectileSpeed, range, penetration);
            }
        }
    }
    public enum StatModType
    {
        Flat,
        PercentAdd,
        PercentMult,
    }
}
