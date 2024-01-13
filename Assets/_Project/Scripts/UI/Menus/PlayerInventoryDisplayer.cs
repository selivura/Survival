using Selivura.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura.UI
{
    public class PlayerInventoryDisplayer : MonoBehaviour
    {
        [SerializeField] ItemDisplayer _prefab;
        [Inject]
        PlayerUnit _playerUnit;
        private List<ItemDisplayer> _spawned = new List<ItemDisplayer>();
        private void Awake()
        {
            FindFirstObjectByType<Injector>().Inject(this);
        }
        private void OnEnable()
        {
            UpdateItems();
        }
        private void UpdateItems()
        {
            for (int i = 0; i < _spawned.Count; i++)
            {
                Destroy(_spawned[i].gameObject);
            }
            _spawned.Clear();
            foreach (var item in _playerUnit.Inventory)
            {
                var displaySpawned = Instantiate(_prefab, transform);
                displaySpawned.ShowItem(item);
                _spawned.Add(displaySpawned);
            }
        }
    }
}
