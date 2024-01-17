using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(Unit))]
    public class UnitMatterReward : MatterSource
    {
        public float _spawnRange = 3;
        public int Amount = 1;
        Unit _unit;
        protected override void Awake()
        {
            base.Awake();
            _unit = GetComponent<Unit>();
        }
        private void SpawnReward(Unit unit)
        {
            for (int i = 0; i < Amount; i++)
            {
                matterSpawner.Spawn(collectibles.GetRandomElement(), Utilities.RandomPositionInRange(transform.position, _spawnRange));
            }
        }
        private void OnEnable()
        {
            _unit.OnKilled.AddListener(SpawnReward);
        }
        private void OnDisable()
        {
            _unit.OnKilled.RemoveListener(SpawnReward);
        }
    }
}
