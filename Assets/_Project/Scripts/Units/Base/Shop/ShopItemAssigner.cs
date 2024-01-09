using UnityEngine;

namespace Selivura
{
    public class ShopItemAssigner : MonoBehaviour
    {
        [SerializeField] Shop _shop;
        [SerializeField] ItemList _itemList;
        private void OnEnable()
        {
            if (_shop != null)
                if (_itemList != null)
                    _shop.ItemPrefab = _itemList.Items[Random.Range(0, _itemList.Items.Count)];
        }
    }
}
