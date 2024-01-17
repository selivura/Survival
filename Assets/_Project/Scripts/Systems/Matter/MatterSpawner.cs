using Selivura.ObjectPooling;
using UnityEngine;

namespace Selivura
{
    public class MatterSpawner : MonoBehaviour, IDependecyProvider
    {
        protected PoolingSystem<MatterCollectible> matterPool;

        [Provide]
        private MatterSpawner Provide()
        {
            return this;
        }
        protected virtual void Awake()
        {
            matterPool = new PoolingSystem<MatterCollectible>(transform);
        }
        public MatterCollectible Spawn(MatterCollectible prefab, Vector2 spawnPos)
        {
            var spawned = matterPool.Get(prefab);
            spawned.transform.position = spawnPos;
            return spawned;
        }
    }
}
