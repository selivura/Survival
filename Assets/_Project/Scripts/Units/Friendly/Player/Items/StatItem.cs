using Selivura.Player;

namespace Selivura
{
    public class StatItem : Item
    {
        public PlayerStatModifier Energy;
        public PlayerStatModifier EnergyRegeneration;
        public PlayerStatModifier EnergyDecay;
        public PlayerStatModifier MovementSpeed;
        public PlayerStatModifier AttackDamage;
        public PlayerStatModifier AttackCooldown;
        public PlayerStatModifier ProjectileSpeed;
        public PlayerStatModifier AttackRange;
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
