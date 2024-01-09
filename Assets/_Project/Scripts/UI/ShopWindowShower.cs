using Selivura.Player;
using UnityEngine;

namespace Selivura.UI
{
    public class ShopWindowShower : MonoBehaviour, IInteractable
    {
        [SerializeField] ShopWindow _prefab;
        [SerializeField] Shop _shop;
        [SerializeField] Vector3 _offset = new Vector3(0, 1);
        ShopWindow _current;
        Timer _timer = new Timer(0, 0);
        [SerializeField] float _hideWindowTime = 2;
        public bool CanInteract(PlayerUnit interactor)
        {
            if (!_current)
            {
                _current = Instantiate(_prefab, OverlayWindowsContainer.Instance.transform);
                _current.Initialize(_shop.ItemPrefab);
                _current.gameObject.AddComponent<PositionLocker>().Initialize(transform.position + _offset);
            }
            _timer = new Timer(_hideWindowTime, Time.time);
            return false;
        }
        private void DespawnWindow()
        {
            _current.Deinitialize();
            _current = null;
        }
        private void FixedUpdate()
        {
            if (_timer.Expired && _current != null)
            {
                DespawnWindow();
            }
        }
        private void OnDisable()
        {
            if (_current != null)
                DespawnWindow();
        }
        public void Interact(PlayerUnit interactor)
        {
            if (_shop != null)
                if (!_shop.CanBuy(interactor.MatterHarvested))
                {
                    if (_current)
                        _current.DoShakeAnimation();
                }
        }
    }
}
