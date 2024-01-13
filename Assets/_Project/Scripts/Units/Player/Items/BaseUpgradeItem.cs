using Selivura.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class BaseUpgradeItem : Item
    {
        [Inject]
        protected MainBase mainBase;
        public override void OnPickup(PlayerUnit player)
        {
            base.OnPickup(player);
            Injector.Instance.Inject(this);
            mainBase.OnLevelUp.AddListener(OnBaseLevelUp);
        }
        public override void OnRemove(PlayerUnit player)
        {
            mainBase.OnLevelUp.RemoveListener(OnBaseLevelUp);
            base.OnRemove(player);
        }
        protected virtual void OnBaseLevelUp()
        {

        }
    }
}
