using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class ItemPoolManager : Singleton<ItemPoolManager>
    {
        [SerializeField] ItemList _itemList;
        List<Item> _unusedItems = new List<Item>();
        protected override void Awake()
        {
            base.Awake();
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
