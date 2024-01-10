using UnityEngine;

namespace Selivura
{
    public class EffectSpawner : MonoBehaviour
    {
        [SerializeField] Effect _effectPrefab;
        [Inject]
        EffectPool _effectPool;
        private void Awake()
        {
            Injector.Instance.Inject(this);
        }
        public void Spawn()
        {
            _effectPool.GetOrCreatedEffect(_effectPrefab).transform.position = transform.position;
        }
    }
}
