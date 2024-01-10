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
        public override void OnPickup(PlayerUnit player)
        {
            base.OnPickup(player);
            player.Energy.AddModifier(Energy);
            player.EnergyRegeneration.AddModifier(EnergyRegeneration);
            player.EnergyDecay.AddModifier(EnergyDecay);
            player.MovementSpeed.AddModifier(MovementSpeed);
            player.AttackDamage.AddModifier(AttackDamage);
            player.AttackCooldown.AddModifier(AttackCooldown);
            player.ProjectileSpeed.AddModifier(ProjectileSpeed);
            player.AttackRange.AddModifier(AttackRange);
        }
        public override void OnRemove(PlayerUnit player)
        {
            base.OnRemove(player);
            player.Energy.RemoveModifier(Energy);
            player.EnergyRegeneration.RemoveModifier(EnergyRegeneration);
            player.EnergyDecay.RemoveModifier(EnergyDecay);
            player.MovementSpeed.RemoveModifier(MovementSpeed);
            player.AttackDamage.RemoveModifier(AttackDamage);
            player.AttackCooldown.RemoveModifier(AttackCooldown);
            player.ProjectileSpeed.RemoveModifier(ProjectileSpeed);
            player.AttackRange.RemoveModifier(AttackRange);
        }
    }
}
