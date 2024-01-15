using Selivura.ObjectPooling;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class BasesSpawner : UnitSpawner
    {
        [SerializeField] Unit[] _bases;
        [SerializeField] private List<Transform> _freeBasePoints = new List<Transform>();

        Dictionary<Unit, Transform> _spawnedBases = new Dictionary<Unit, Transform>();
        public void OnPhaseChange(PhaseType type)
        {
            if (type != PhaseType.Peace) return;

            if (_freeBasePoints.Count <= 0)
            {
                Debug.Log("No free base points!");
                return;
            }

            Transform spawnPoint = _freeBasePoints.GetRandomElement();
            Unit selected = _bases.GetRandomElement();

            Unit spawned = Spawn(selected, spawnPoint.position);

            spawned.OnDeinitialized.AddListener(RemoveFromSpawnedUnits);

            _spawnedBases.Add(spawned, spawnPoint);
            _freeBasePoints.Remove(spawnPoint);
        }

        private void RemoveFromSpawnedUnits(Unit unit)
        {
            _spawnedBases.Remove(unit, out Transform freeSpawnPoint);
            _freeBasePoints.Add(freeSpawnPoint);
            unit.OnDeinitialized.RemoveListener(RemoveFromSpawnedUnits);
        }
    }
}
