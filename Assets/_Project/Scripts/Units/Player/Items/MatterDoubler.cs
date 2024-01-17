using Selivura.Player;
using UnityEngine;

namespace Selivura
{
    public class MatterDoubler : OneUseItem
    {
        [SerializeField]
        public override void OnPickup(PlayerUnit player)
        {
            base.OnPickup(player);
            player.ChangeMatter(Price);
            player.ChangeMatter(player.MatterHarvested);
        }
    }
}
