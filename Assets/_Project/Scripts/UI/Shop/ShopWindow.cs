using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.UI
{
    public class ShopWindow : MonoBehaviour
    {
        [SerializeField] TMP_Text _itemNameText;
        [SerializeField] TMP_Text _itemDescriptionText;
        [SerializeField] TMP_Text _itemPriceText;
        [SerializeField] Image _itemImage;

        public GameObject Container;
        [SerializeField] float _windowShakeDuration = 0.5f;
        [SerializeField] float _windowShowDuration = 0.8f;
        [SerializeField] float _windowHideDuration = 0.25f;
        public void Initialize(Item item)
        {
            _itemNameText.text = item.DisplayName;
            _itemDescriptionText.text = item.Description;
            _itemPriceText.text = item.Price.ToString();
            DoShowAnimation();
            _itemImage.sprite = item.Icon;
        }
        public void Initialize(string name, string desc, int price, Sprite picture)
        {
            _itemNameText.text = name;
            _itemDescriptionText.text = desc;
            _itemPriceText.text = price.ToString();
            DoShowAnimation();
            _itemImage.sprite = picture;
        }
        public void DoShakeAnimation()
        {
            Container.transform.DOShakePosition(_windowShakeDuration);
        }
        public void DoShowAnimation()
        {
            Container.transform.localScale = new Vector3(0, 0, 1);
            Container.transform.DOScale(new Vector3(1, 1), _windowShowDuration);
        }
        public void HideWindow()
        {
            Container.transform.DOScale(new Vector3(0, 0, 1), _windowHideDuration);
            Invoke(nameof(Deinitialize), _windowHideDuration);
        }
        private void Deinitialize()
        {
            Container.transform.DOComplete();
            Destroy(gameObject, _windowHideDuration);
        }
    }
}
