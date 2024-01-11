using Selivura.Player;
using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(Collider2D))]
    public class EnergyRegenArea : InfiniteEnergyArea
    {
        [SerializeField] private float _rechargeCooldown = .1f;
        private Timer _rechargeTimer = new Timer(0,0);
        public bool CanCharge => _rechargeTimer.Expired && playerInRange;
       
        protected override void OnTriggerStay2D(Collider2D collision)
        {
            base.OnTriggerStay2D(collision);
            if (collision.gameObject == _player.gameObject)
            {
                playerInRange = true;
            }
        }
        protected override void OnTriggerExit2D(Collider2D collision)
        {
            base.OnTriggerExit2D(collision);
            if (!_player) return;
            if (collision.gameObject == _player.gameObject)
            {
                playerInRange = false;
            }
        }
        private void ChargePlayerEnergy()
        {
            if (CanCharge)
            {
                _player.ChangeEnergy(_player.EnergyRegeneration.Value * _rechargeCooldown);
                _rechargeTimer = new Timer(_rechargeCooldown, Time.time);
            }
        }
        private void FixedUpdate()
        {
            ChargePlayerEnergy();
        }
    }
}
