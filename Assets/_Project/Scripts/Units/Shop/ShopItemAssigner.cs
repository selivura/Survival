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
            Injector.Instance.Inject(this);
            _shop = GetComponent<Shop>();
        }
        private void OnEnable()
        {
            if (_shop != null)
                _shop.ItemPrefab = _itemPoolManager.GetRandomUnusedItem();
        }
    }
}
