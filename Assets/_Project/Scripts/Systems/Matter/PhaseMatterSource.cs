using UnityEngine;

namespace Selivura
{
    public class PhaseMatterSource : MatterSource
    {
        [SerializeField] private int _spawnsPerPhase = 5;

        public float SpawnMaxRange = 25;
        public float SpawnMinRange = 10;
        private void SpawnInRange()
        {
            for (int i = 0; i < _spawnsPerPhase; i++)
            {
                var spawnPos = Utilities.RandomPositionInRangeLimited(transform.position, SpawnMinRange, SpawnMaxRange);
                var spawned = matterSpawner.Spawn(collectibles.GetRandomElement(), spawnPos);
                spawned.transform.position = spawnPos;
            }
        }
        public void OnPhaseChange(PhaseType type)
        {
            if (type != PhaseType.Peace)
                return;
            SpawnInRange();
        }
    }
}
