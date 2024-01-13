using UnityEngine;

namespace Selivura.UI
{
    [RequireComponent(typeof(Shop))]
    public class ShopBuyAttention : MonoBehaviour
    {
        Shop _shop;
        [Inject]
        AttentionMessage _attentionMessage;
        private void Awake()
        {
            _shop = GetComponent<Shop>();   
            FindFirstObjectByType<Injector>().Inject(this);
        }
        private void OnEnable()
        {
            _shop.OnPurchase.AddListener(ShowPurchaseMessage);
        }
        private void OnDisable()
        {
            _shop.OnPurchase.RemoveListener(ShowPurchaseMessage);
        }
        private void ShowPurchaseMessage()
        {
            _attentionMessage.ShowMessage($"ITEM ADDED: {_shop.ItemPrefab.DisplayName}");
        }
    }
}
