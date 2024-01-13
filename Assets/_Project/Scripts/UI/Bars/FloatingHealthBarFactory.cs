using UnityEngine;

namespace Selivura.UI
{
    [CreateAssetMenu(menuName = "Factory / FloatingHealthBarFactory")]
    public class FloatingHealthBarFactory : ScriptableObject
    {
        [SerializeField] UnitHPFloatingBar _prefab;

        public UnitHPFloatingBar CreateAndSetParent(Transform parent)
        {
            UnitHPFloatingBar spawned = Instantiate(_prefab, parent);
            return spawned;
        }
    }
}
