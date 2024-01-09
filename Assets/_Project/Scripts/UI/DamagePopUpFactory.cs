using UnityEngine;

namespace Selivura.UI
{
    [CreateAssetMenu(menuName = "Factory / DamagePopUpFactory")]
    public class DamagePopUpFactory : ScriptableObject
    {
        [SerializeField] DamagePopUp _prefab;

        public DamagePopUp CreateAndSetPosition(Vector2 position)
        {
            DamagePopUp spawned = Instantiate(_prefab, position, Quaternion.identity);
            return spawned;
        }
    }
}
