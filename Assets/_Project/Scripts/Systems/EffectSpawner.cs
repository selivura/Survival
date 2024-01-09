using UnityEngine;

namespace Selivura
{
    public class EffectSpawner : MonoBehaviour
    {
        [SerializeField] Effect _effectPrefab;

        public void Spawn()
        {
            EffectPool.Instance.GetOrCreatedEffect(_effectPrefab).transform.position = transform.position;
        }
    }
}
