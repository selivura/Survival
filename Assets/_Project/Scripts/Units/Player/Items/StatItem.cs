using Selivura.Player;

namespace Selivura
{
    public class StatItem : Item
    {
        public PlayerStatModifier Energy = new PlayerStatModifier(0, StatModType.Flat, 0, "Item");
        public PlayerStatModifier EnergyRegeneration = new PlayerStatModifier(0, StatModType.Flat, 0, "Item");
        public PlayerStatModifier EnergyDecay = new PlayerStatModifier(0, StatModType.Flat, 0, "Item");
        public PlayerStatModifier MovementSpeed = new PlayerStatModifier(0, StatModType.Flat, 0, "Item");
        public PlayerStatModifier AttackDamage = new PlayerStatModifier(0, StatModType.Flat, 0, "Item");
        public PlayerStatModifier AttackCooldown = new PlayerStatModifier(0, StatModType.Flat, 0, "Item");
        public PlayerStatModifier ProjectileSpeed = new PlayerStatModifier(0, StatModType.Flat, 0, "Item");
        public PlayerStatModifier AttackRange = new PlayerStatModifier(0, StatModType.Flat, 0, "Item");
        public PlayerStatModifier Penetration = new PlayerStatModifier(0, StatModType.Flat, 0, "Item");
        public override void OnPickup(PlayerUnit player)
        {
            base.OnPickup(player);
            var playerStats = player.PlayerStats;
            playerStats.Energy.AddModifier(Energy);
            playerStats.EnergyRegeneration.AddModifier(EnergyRegeneration);
            playerStats.EnergyDecay.AddModifier(EnergyDecay);
            playerStats.MovementSpeed.AddModifier(MovementSpeed);
            playerStats.AttackDamage.AddModifier(AttackDamage);
            playerStats.AttackCooldown.AddModifier(AttackCooldown);
            playerStats.ProjectileSpeed.AddModifier(ProjectileSpeed);
            playerStats.AttackRange.AddModifier(AttackRange);
            playerStats.Penetration.AddModifier(Penetration);
        }
        public override void OnRemove(PlayerUnit player)
        {
            base.OnRemove(player);
            var playerStats = player.PlayerStats;
            playerStats.Energy.RemoveModifier(Energy);
            playerStats.EnergyRegeneration.RemoveModifier(EnergyRegeneration);
            playerStats.EnergyDecay.RemoveModifier(EnergyDecay);
            playerStats.MovementSpeed.RemoveModifier(MovementSpeed);
            playerStats.AttackDamage.RemoveModifier(AttackDamage);
            playerStats.AttackCooldown.RemoveModifier(AttackCooldown);
            playerStats.ProjectileSpeed.RemoveModifier(ProjectileSpeed);
            playerStats.AttackRange.RemoveModifier(AttackRange);
            playerStats.Penetration.RemoveModifier(Penetration);
        }
    }
}
