using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class ItemPoolManager : MonoBehaviour, IDependecyProvider
    {
        [SerializeField] ItemList _itemList;
        List<Item> _unusedItems = new List<Item>();

        [Provide]
        protected ItemPoolManager Provide()
        {
            return this;
        }
        protected void Awake()
        {
            _unusedItems.AddRange(_itemList.Items);
        }
        public Item GetRandomUnusedItem()
        {
            if (_unusedItems.Count <= 0)
                return _itemList.Items[0];

            var item = _unusedItems.GetRandomElement();
            _unusedItems.Remove(item);
            return item;
        }
    }
}
