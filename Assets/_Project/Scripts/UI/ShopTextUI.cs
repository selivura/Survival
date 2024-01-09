using UnityEngine;

namespace Selivura.UI
{
    public class ShopTextUI : TextDisplayUI
    {
        [SerializeField] Shop _shop;
        private void Start()
        {
            tmpText.text = prefix + $"{_shop.ItemPrefab.DisplayName}\n${_shop.ItemPrefab.Price}";
        }
    }
}
