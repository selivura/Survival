using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    [CreateAssetMenu(menuName = "Item list")]
    public class ItemList : ScriptableObject
    {
        public List<Item> Items = new List<Item>();
    }
}
