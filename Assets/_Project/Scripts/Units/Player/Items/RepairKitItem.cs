using Selivura.Player;
using UnityEngine;

namespace Selivura
{
    public class RepairKitItem : OneUseItem
    {
        [SerializeField] protected int _healAmount = 50;
        [Inject]
        MainBase _base;
        public override void OnPickup(PlayerUnit player)
        {
            base.OnPickup(player);
            FindFirstObjectByType<Injector>().Inject(this);
            _base.Heal(_healAmount);
        }

    }
}
