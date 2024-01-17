using Selivura.Player;
using UnityEngine;

namespace Selivura
{
    public class DamageAreaUpgrade : BaseUpgradeItem
    {
        [SerializeField] protected int DpsPerBaseLevel = 2;
        [SerializeField] protected float BaseRange = 2f;
        [SerializeField] protected float RangePerBaseLevel = 0.25f;
        [SerializeField] protected DamageOverTimeArea areaPrefab;
        protected DamageOverTimeArea spawnedArea;
        public override void OnPickup(PlayerUnit player)
        {
            base.OnPickup(player);
            spawnedArea = Instantiate(areaPrefab, mainBase.transform);
            OnBaseLevelUp();
        }
        protected override void OnBaseLevelUp()
        {
            base.OnBaseLevelUp();
            spawnedArea.DamagePerSecond = mainBase.Level * DpsPerBaseLevel;
            spawnedArea.Radius = BaseRange + mainBase.Level * RangePerBaseLevel;
        }
        public override void OnRemove(PlayerUnit player)
        {
            base.OnRemove(player);
            Destroy(spawnedArea);
        }
    }
}
