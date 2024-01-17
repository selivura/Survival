using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(Collider2D))]
    public class EnergyRegenArea : InfiniteEnergyArea
    {
        [SerializeField] private float _rechargeCooldown = .1f;
        private Timer _rechargeTimer = new Timer(0, 0);
        public bool CanCharge => _rechargeTimer.Expired && playerInRange;
        private void ChargePlayerEnergy()
        {
            _player.ChangeEnergy(_player.PlayerStats.EnergyRegeneration.Value * _rechargeCooldown);
            _rechargeTimer = new Timer(_rechargeCooldown, Time.time);
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (CanCharge)
            {
                ChargePlayerEnergy();
            }
        }
    }
}
