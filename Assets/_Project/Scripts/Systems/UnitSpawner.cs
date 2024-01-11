using Selivura.ObjectPooling;
using UnityEngine;

namespace Selivura
{
    public abstract class UnitSpawner : MonoBehaviour
    {
        [SerializeField] Effect _unitSpawnEffect;
        protected PoolingSystem<Unit> pool;
        private void Awake()
        {
            pool = new PoolingSystem<Unit>(transform);
        }

        public virtual Unit Spawn(Unit prefab, Vector2 position)
        {
            Unit spawned = pool.Get(prefab);
            spawned.transform.position = position;

            if(_unitSpawnEffect != null)
            {
                Instantiate(_unitSpawnEffect, spawned.transform);
            }

            return spawned;
        }
    }
}
