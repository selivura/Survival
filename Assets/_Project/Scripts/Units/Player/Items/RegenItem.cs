using Selivura.Player;
using UnityEngine;

namespace Selivura
{
    public class RegenItem : Item
    {
        [SerializeField] protected int _regenPerSecond = 1;
        protected Timer _regenTimer = new Timer(0, 0);
        public override void OnPickup(PlayerUnit player)
        {
            base.OnPickup(player);
            _regenTimer = new Timer(1, Time.time);
        }
        private void FixedUpdate()
        {
            if(_regenTimer.Expired)
            {
                _regenTimer = new Timer(1, Time.time);
                playerUnit.Heal(_regenPerSecond);
            }
        }
    }
}
