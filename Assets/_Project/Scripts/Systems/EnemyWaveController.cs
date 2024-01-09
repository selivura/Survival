using Selivura.ObjectPooling;
using Selivura.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public enum PhaseType
    {
        Peace,
        Defence,
    }
    public class EnemyWaveController : MonoBehaviour, IDependecyProvider
    {
        [SerializeField] private float _minEnemySpwanRange = 15;
        [SerializeField] private float _maxEnemySpwanRange = 25;

        [SerializeField] private Vector2 _enemySpwanLimitation = new Vector2(50, 50);

        [SerializeField] private float _peacePhaseTimeSeconds = 60;
        [SerializeField] private WaveData[] _waveDatas;
        private WaveData _currentWaveData;
        [SerializeField] private int _spawnCooldownSeconds = 1;
        private PoolingSystem<Unit> _enemyPool;
        public PhaseType CurrentPhase;
        private float _phaseStartTime;
        public float PhaseTimer => Time.time - _phaseStartTime;
        public float PeacePhaseTimeLeft => _peacePhaseTimeSeconds - PhaseTimer;
        private bool _canSpawnEnemy => Time.time - _lastEnemySpawnTime > _spawnCooldownSeconds && !_enemyLimitReached;
        private bool _enemyLimitReached => _currentSpawnIndex >= EnemiesPerWave;
        private float _lastEnemySpawnTime;
        private int _currentSpawnIndex = 0;
        private List<Unit> _spawnedUnits = new List<Unit>();

        public delegate void OnPahseChangeDelegate(PhaseType type);
        public event OnPahseChangeDelegate OnPhaseChange;

        private float _startTime;
        public float TotalSurvivalTime => Time.time - _startTime;
        public int CurrentWave { get; private set; }
        public int EnemyHealthPerWavePercent = 2;
        public int EnemiesPerWave = 3;
        public int _phaseIndex = 0;

        [Inject]
        PlayerUnit _playerUnit;

        [Provide]
        public EnemyWaveController Provide()
        {
            return this;
        }
        private void Awake()
        {
            _enemyPool = new PoolingSystem<Unit>(transform);
        }
        private void Start()
        {
            _startTime = Time.time;
            StartPeacePhase();
        }
        private void FixedUpdate()
        {
            if (CurrentPhase == PhaseType.Peace)
            {
                if (PeacePhaseTimeLeft <= 0)
                {
                    StartDefencePhase();
                }
            }
            if (CurrentPhase == PhaseType.Defence)
            {
                if (_canSpawnEnemy)
                    SpawnEnemy();
                if (_spawnedUnits.Count <= 0 && _enemyLimitReached)
                {
                    StartPeacePhase();
                }
            }
        }
        private void StartDefencePhase()
        {
            _currentSpawnIndex = 0;
            CurrentPhase = PhaseType.Defence;
            _phaseStartTime = Time.time;
            _currentWaveData = _waveDatas[Random.Range(0, _waveDatas.Length)];
            SpawnEnemy();
            OnPhaseChange?.Invoke(CurrentPhase);
            _phaseIndex++;
        }
        private void StartPeacePhase()
        {
            CurrentPhase = PhaseType.Peace;
            _phaseStartTime = Time.time;
            OnPhaseChange?.Invoke(CurrentPhase);
            if (_phaseIndex > 0)
            {
                EnemiesPerWave++;
                CurrentWave++;
            }
            _phaseIndex++;
        }
        private void SpawnEnemy()
        {
            if (!_canSpawnEnemy)
                return;

            var spawned = _enemyPool.Get(_currentWaveData.WaveEnemies[Random.Range(0, _currentWaveData.WaveEnemies.Length)]);

            var spawnPosition = Utilities.RandomPositionInRangeLimited(_playerUnit.transform.position, _minEnemySpwanRange, _maxEnemySpwanRange);
            while (
                spawnPosition.x > _enemySpwanLimitation.x / 2
                || spawnPosition.y > _enemySpwanLimitation.y / 2
                || spawnPosition.y < -_enemySpwanLimitation.y / 2
                || spawnPosition.x < -_enemySpwanLimitation.x / 2)
            {
                spawnPosition = Utilities.RandomPositionInRangeLimited(_playerUnit.transform.position, _minEnemySpwanRange, _maxEnemySpwanRange);
            }
            spawned.transform.position = spawnPosition;

            spawned.OnKilled.AddListener(RemoveEnemyFromList);
            spawned.Initialize(Mathf.RoundToInt(spawned.BaseHealth * (CurrentWave * EnemyHealthPerWavePercent / 100f)));
            _spawnedUnits.Add(spawned);
            _lastEnemySpawnTime = Time.time;
            _currentSpawnIndex++;
        }
        private void RemoveEnemyFromList(Unit unit)
        {
            _spawnedUnits.Remove(unit);
            unit.OnKilled.RemoveListener(RemoveEnemyFromList);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, _enemySpwanLimitation);
        }
    }
}
