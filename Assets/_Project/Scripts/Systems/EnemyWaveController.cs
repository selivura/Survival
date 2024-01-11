using Selivura.ObjectPooling;
using Selivura.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public enum PhaseType
    {
        Peace,
        Defence,
    }
    public class EnemyWaveController : UnitSpawner, IDependecyProvider
    {
        [Header("Spawn limits")]
        [SerializeField] private float _minEnemySpwanRange = 15;
        [SerializeField] private float _maxEnemySpwanRange = 25;
        [SerializeField] private Vector2 _enemySpwanLimitation = new Vector2(50, 50);

        [Header("Peaceful phase settings")]
        [SerializeField] private float _peacePhaseTime = 30;

        [Header("Enemy spawn settings")]
        [SerializeField] private int _enemySpawnCooldownSeconds = 1;
        [SerializeField] private WaveData[] _waveDatas;
        private WaveData _currentWaveData;

        public PhaseType CurrentPhaseType;

        private Timer _phaseTimer = new Timer(0,0);
        public float PhaseTimeLeft => _phaseTimer.TimeLeft;
        private Timer _enemySpawnTimer = new Timer(0, 0);
        private bool _canSpawnEnemy => _enemySpawnTimer.Expired && !_enemyLimitReached;

        private bool _enemyLimitReached => _currentSpawnIndex >= _currentWaveData.WaveEnemies.Length;
        private int _currentSpawnIndex = 0;
        private List<Unit> _spawnedUnits = new List<Unit>();

        private float _startTime;
        public float TotalSurvivalTime => Time.time - _startTime;
        public int CurrentWaveIndex { get; private set; }
        public float EnemyHealthPerDifficultyMultiplier = 1.5f;
        public int Loop { get; private set; } = 0;

        public UnityEvent OnWaveStarted;
        public UnityEvent<PhaseType> OnPhaseChange;

        [Inject]
        PlayerUnit _playerUnit;

        [Provide]
        public EnemyWaveController Provide()
        {
            return this;
        }
        private void Start()
        {
            _startTime = Time.time;
            StartPeacePhase();
        }
        private void FixedUpdate()
        {
            if (CurrentPhaseType == PhaseType.Peace)
            {
                if (_phaseTimer.Expired)
                {
                    StartDefencePhase();
                }
            }
            if (CurrentPhaseType == PhaseType.Defence)
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
            CurrentPhaseType = PhaseType.Defence;

            _currentSpawnIndex = 0; 
            CurrentWaveIndex++;

            if (CurrentWaveIndex >= _waveDatas.Length)
            {
                Loop++;
                CurrentWaveIndex = 0;
            }

            _currentWaveData = _waveDatas[CurrentWaveIndex];
            SpawnEnemy();
            OnPhaseChange?.Invoke(CurrentPhaseType);
            OnWaveStarted?.Invoke();
            
        }
        private void StartPeacePhase()
        {
            _phaseTimer = new Timer(_peacePhaseTime, Time.time);
            CurrentPhaseType = PhaseType.Peace;
            OnPhaseChange?.Invoke(CurrentPhaseType);
        }
        private void SpawnEnemy()
        {
            if (!_canSpawnEnemy)
                return;

            Vector2 spawnPosition = GetValidSpawnPosition(
                _playerUnit.transform.position,
                _enemySpwanLimitation,
                _minEnemySpwanRange,
                _maxEnemySpwanRange);

            var spawned = Spawn(_currentWaveData.WaveEnemies[_currentSpawnIndex], spawnPosition);

            spawned.transform.position = spawnPosition;
            spawned.OnKilled.AddListener(RemoveEnemyFromList);
            spawned.Initialize(Mathf.RoundToInt(spawned.BaseHealth * (Loop * EnemyHealthPerDifficultyMultiplier)));

            _spawnedUnits.Add(spawned);

            _enemySpawnTimer = new Timer(_enemySpawnCooldownSeconds, Time.time);
            _currentSpawnIndex++;
        }
        private Vector2 GetValidSpawnPosition(Vector2 position, Vector2 limit, float minRange, float maxRange)
        {
            var spawnPosition = Utilities.RandomPositionInRangeLimited(position, minRange, maxRange);
            while (
                spawnPosition.x > limit.x / 2
                || spawnPosition.y > limit.y / 2
                || spawnPosition.y < -limit.y / 2
                || spawnPosition.x < -limit.x / 2)
            {
                spawnPosition = Utilities.RandomPositionInRangeLimited(position, minRange, maxRange);
            }

            return spawnPosition;
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
