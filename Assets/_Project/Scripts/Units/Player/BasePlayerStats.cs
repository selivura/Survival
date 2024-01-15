using UnityEngine;

namespace Selivura
{
    [CreateAssetMenu(menuName ="Player/Stats")]
    public class BasePlayerStats : ScriptableObject
    {
        public float EnergyDecayCooldown = .1f;
        public float NoEnergyHealthDamage = 25;
        public float Energy = 75;
        public float EnergyRegeneration = 25;
        public float EnergyDecay = 4;
        public float MovementSpeed = 5;
        public int AttackDamage = 10;
        public float AttackCooldown = 0.4f;
        public float ProjectileSpeed = 15;
        public float AttackRange = 8;
        public int Penetration = 2;
    }
}
