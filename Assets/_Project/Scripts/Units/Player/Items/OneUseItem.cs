using Selivura.Player;
using UnityEngine;

namespace Selivura
{
    public abstract class OneUseItem : Item
    {
        [SerializeField] protected Sprite _brokenSprite;
        [SerializeField] protected string _brokenName = "Used repair kit";
        public override void OnPickup(PlayerUnit player)
        {
            base.OnPickup(player);
            Icon = _brokenSprite;
            DisplayName = _brokenName;
        }
    }
}
