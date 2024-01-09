using Selivura.ObjectPooling;
using UnityEngine;

namespace Selivura
{
    public class MatterSpawner : MonoBehaviour
    {
        [SerializeField] MatterCollectible _prefab;
        private PoolingSystem<MatterCollectible> _matterPool;
        private EnemyWaveController _waveController;
        [SerializeField] private int _spawnsPerPhase = 5;
        public float SpawnMaxRange = 25;
        public float SpawnMinRange = 10;
        private void Awake()
        {
            _matterPool = new PoolingSystem<MatterCollectible>(transform);
            _waveController = FindAnyObjectByType<EnemyWaveController>();
            _waveController.OnPhaseChange += OnPhaseChange;
        }
        private void OnDestroy()
        {
            _waveController.OnPhaseChange -= OnPhaseChange;
        }
        private void Spawn()
        {
            for (int i = 0; i < _spawnsPerPhase; i++)
            {
                var spawnPos = new Vector2(Random.Range(-GetRandomClampedPoint(), GetRandomClampedPoint()), Random.Range(-GetRandomClampedPoint(), GetRandomClampedPoint()));
                var spawned = _matterPool.Get(_prefab);
                spawned.transform.position = spawnPos;
            }
        }

        private float GetRandomClampedPoint()
        {
            return Mathf.Clamp(Random.Range(0, SpawnMaxRange), SpawnMinRange, SpawnMaxRange);
        }

        private void OnPhaseChange(PhaseType type)
        {
            if (type != PhaseType.Peace)
                return;
            Spawn();
        }
    }
}
