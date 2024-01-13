using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.UI
{
    public class ItemDisplayer : MonoBehaviour
    {
        [SerializeField] TMP_Text _itemNameText;
        [SerializeField] TMP_Text _itemDescriptionText;
        [SerializeField] Image _itemImage;

        public void ShowItem(Item item)
        {
            _itemDescriptionText.text = item.Description;
            _itemImage.sprite = item.Icon;
            _itemNameText.text = item.DisplayName;
        }
    }
}
