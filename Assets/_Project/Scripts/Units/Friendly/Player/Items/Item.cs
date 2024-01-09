using Selivura.Player;
using UnityEngine;

namespace Selivura
{
    public abstract class Item : MonoBehaviour
    {
        public string DisplayName = "Item";
        public string Description = "Description";
        public int Price = 10;
        protected PlayerUnit playerUnit;
        public virtual void OnPickup(PlayerUnit player)
        {
            playerUnit = player;
        }
        public virtual void OnRemove(PlayerUnit player)
        {

        }
    }
}
