using Selivura.ObjectPooling;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class BasesSpawner : MonoBehaviour
    {
        [SerializeField] FriendlyBase[] _bases;
        [SerializeField] private List<Transform> _freeBasePoints = new List<Transform>();
        PoolingSystem<FriendlyBase> _pool;

        [Inject]
        EnemyWaveController _enemyWaveController;

        Dictionary<FriendlyBase, Transform> _spawnedBases = new Dictionary<FriendlyBase, Transform>();
        private void Awake()
        {
            _pool = new PoolingSystem<FriendlyBase>(transform);
        }
        private void OnEnable()
        {
            _enemyWaveController.OnPhaseChange += OnPhaseChange;
        }

        private void OnPhaseChange(PhaseType type)
        {
            if (type == PhaseType.Peace)
            {
                Spawn();
            }
        }

        public void Spawn()
        {
            FriendlyBase selectedShop = _bases.GetRandomElement();
            if (_freeBasePoints.Count <= 0)
            {
                Debug.Log("No base free base points!");
                return;
            }
            Transform spawnPos = _freeBasePoints.GetRandomElement();
            FriendlyBase spawned = _pool.Get(selectedShop);
            spawned.transform.position = spawnPos.position;
            spawned.OnDeinitialized.AddListener(RemoveFromSpawnedUnits);
            _spawnedBases.Add(spawned, spawnPos);
            _freeBasePoints.Remove(spawnPos);
        }

        private void RemoveFromSpawnedUnits(Unit unit)
        {
            _spawnedBases.Remove((FriendlyBase)unit, out Transform freeSpawnPoint);
            _freeBasePoints.Add(freeSpawnPoint);
            unit.OnDeinitialized.RemoveListener(RemoveFromSpawnedUnits);
        }

        //private Vector3 GetRandomSpawnPos()
        //{
        //    Vector3 pos = Vector3.zero;
        //    pos = Utilities.RandomPositionInRangeLimited(pos, _minSpawnRange, _spawnRange);
        //    int cycleAmount = 0;
        //    foreach (var item in _spawnedBases)
        //    {
        //        if (!item.gameObject.activeSelf)
        //            continue;
        //        while (Vector3.Distance(item.transform.position, pos) > _distanceBetweenShops)
        //        {
        //            cycleAmount++;
        //            pos = new Vector3(Random.Range(-_spawnRange, _spawnRange), Random.Range(-_spawnRange, _spawnRange));
        //            if (cycleAmount > 10000000)
        //                throw new System.Exception("Infinite cycle");
        //        }
        //    }
        //    return pos;
        //}
    }
}
