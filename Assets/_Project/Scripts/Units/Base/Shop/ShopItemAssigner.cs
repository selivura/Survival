using UnityEngine;

namespace Selivura
{
    public class ShopItemAssigner : MonoBehaviour
    {
        [SerializeField] Shop _shop;
        private void OnEnable()
        {
            if (_shop != null)
                _shop.ItemPrefab = ItemPoolManager.Instance.GetRandomUnusedItem();
        }
    }
}
