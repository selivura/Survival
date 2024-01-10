using UnityEngine;

namespace Selivura
{
    public class ShopItemAssigner : MonoBehaviour
    {
        [SerializeField] Shop _shop;

        [Inject]
        ItemPoolManager _itemPoolManager;
        private void Awake()
        {
            Injector.Instance.Inject(this);
        }
        private void OnEnable()
        {
            if (_shop != null)
                _shop.ItemPrefab = _itemPoolManager.GetRandomUnusedItem();
        }
    }
}
