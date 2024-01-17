using Selivura.ObjectPooling;
using Selivura.Player;
using UnityEngine;

namespace Selivura
{
    public class RicoshotBullets : Item
    {
        [SerializeField] Ricoshot _prefab;
        ObjectPool<Ricoshot> _pool;
        [SerializeField] private PlayerStatModifier _piercingMod = new PlayerStatModifier(0, StatModType.PercentMult, 0, "Item");
        public override void OnPickup(PlayerUnit player)
        {
            base.OnPickup(player);
            player.GetComponent<Combat>().OnHit.AddListener(OnHit);
            _pool = new ObjectPool<Ricoshot>(_prefab, 1);
            player.PlayerStats.Penetration.AddModifier(_piercingMod);
        }
        public override void OnRemove(PlayerUnit player)
        {
            base.OnRemove(player);
            player.GetComponent<Combat>().OnHit.RemoveListener(OnHit);
            player.PlayerStats.Penetration.RemoveModifier(_piercingMod);
        }
        void OnHit(HitInfo info)
        {
            var spawned = _pool.GetFreeElement();
            spawned.transform.position = info.unit.transform.position;
        }
    }
}
