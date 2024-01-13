using Selivura.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class RepairKitItem : BaseUpgradeItem
    {
        [SerializeField] protected int _healAmount = 50;
        public override void OnPickup(PlayerUnit player)
        {
            base.OnPickup(player);
            mainBase.Heal(_healAmount);
        }

    }
}
