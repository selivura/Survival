using Selivura.ObjectPooling;
using UnityEngine;

namespace Selivura
{
    public class MatterSpawner : MonoBehaviour
    {
        [SerializeField] MatterCollectible _prefab;
        [SerializeField] private int _spawnsPerPhase = 5;
        private PoolingSystem<MatterCollectible> _matterPool;

        public float SpawnMaxRange = 25;
        public float SpawnMinRange = 10;
        private void Awake()
        {
            _matterPool = new PoolingSystem<MatterCollectible>(transform);
        }
        private void Spawn()
        {
            for (int i = 0; i < _spawnsPerPhase; i++)
            {
                var spawnPos = Utilities.RandomPositionInRangeLimited(transform.position, SpawnMinRange, SpawnMaxRange);
                var spawned = _matterPool.Get(_prefab);
                spawned.transform.position = spawnPos;
            }
        }
        public void OnPhaseChange(PhaseType type)
        {
            if (type != PhaseType.Peace)
                return;
            Spawn();
        }
    }
}
