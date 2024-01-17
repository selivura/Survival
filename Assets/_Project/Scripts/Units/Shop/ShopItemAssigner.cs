using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(Shop))]
    public class ShopItemAssigner : MonoBehaviour
    {
        Shop _shop;

        [Inject]
        ItemPoolManager _itemPoolManager;
        private void Awake()
        {
            FindFirstObjectByType<Injector>().Inject(this);
            _shop = GetComponent<Shop>();
        }
        private void OnEnable()
        {
            if (_shop != null)
                _shop.ItemPrefab = _itemPoolManager.GetRandomUnusedItem();
        }
    }
}
