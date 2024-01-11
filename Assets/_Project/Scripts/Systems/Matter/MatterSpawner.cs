using Selivura.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class MatterSpawner : MonoBehaviour, IDependecyProvider
    {
        [SerializeField] protected MatterCollectible prefab;
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
        public MatterCollectible Spawn(Vector2 spawnPos)
        {
            var spawned = matterPool.Get(prefab);
            spawned.transform.position = spawnPos;
            return spawned;
        }
    }
}
