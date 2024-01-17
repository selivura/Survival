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
        public SurvivalSettings Settings;
        private WaveData _currentWaveData;

        public PhaseType CurrentPhaseType;

        private Timer _phaseTimer = new Timer(0, 0);
        public float PhaseTimeLeft => _phaseTimer.TimeLeft;
        private Timer _enemySpawnTimer = new Timer(0, 0);
        private bool _canSpawnEnemy => _enemySpawnTimer.Expired && !_enemyLimitReached;

        private bool _enemyLimitReached => _currentSpawnIndex >= _currentWaveData.WaveEnemies.Length;
        private int _currentSpawnIndex = 0;
        private List<Unit> _spawnedUnits = new List<Unit>();

        private float _startTime;
        public float TotalSurvivalTime => Time.time - _startTime;
        public int CurrentWaveIndex { get; private set; }

        public int TotalWaves => CurrentWaveIndex + Loop * Settings.WaveDatas.Length;
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
                    SpawnEnemyEntry();
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

            _currentWaveData = Settings.WaveDatas[CurrentWaveIndex];
            CurrentWaveIndex++;
            if (CurrentWaveIndex >= Settings.WaveDatas.Length)
            {
                Loop++;
                CurrentWaveIndex = 0;
                _currentWaveData = Settings.WaveDatas[CurrentWaveIndex];
                CurrentWaveIndex++;
            }
            SpawnEnemyEntry();
            OnPhaseChange?.Invoke(CurrentPhaseType);
            OnWaveStarted?.Invoke();

        }
        private void StartPeacePhase()
        {
            _phaseTimer = new Timer(Settings.PeacePhaseTime, Time.time);
            CurrentPhaseType = PhaseType.Peace;
            OnPhaseChange?.Invoke(CurrentPhaseType);
        }
        private void SpawnEnemyEntry()
        {
            if (!_canSpawnEnemy)
                return;
            for (int i = 0; i < _currentWaveData.WaveEnemies[_currentSpawnIndex].Amount; i++)
            {
                SpawnEnemy();
            }
            _enemySpawnTimer = new Timer(Settings.EnemySpawnCooldown, Time.time);
            _currentSpawnIndex++;
        }

        private void SpawnEnemy()
        {
            Vector2 spawnPosition = GetValidSpawnPosition(
            _playerUnit.transform.position,
            Settings.EnemySpwanLimitation,
            Settings.MinEnemySpwanRange,
            Settings.MaxEnemySpwanRange,
            Settings.MinSpawnRangeFromBase);
            var spawned = Spawn(_currentWaveData.WaveEnemies[_currentSpawnIndex].EnemyPrefab, spawnPosition);

            spawned.transform.position = spawnPosition;
            spawned.OnKilled.AddListener(RemoveEnemyFromList);
            spawned.Initialize(Mathf.RoundToInt(spawned.BaseHealth * (Loop * Settings.EnemyHealthPerDifficultyMultiplier)));
            _spawnedUnits.Add(spawned);
        }

        private Vector2 GetValidSpawnPosition(Vector2 position, Vector2 limit, float minRange, float maxRange, float baseRange)
        {
            var spawnPosition = Utilities.RandomPositionInRangeLimited(position, minRange, maxRange);
            while (
                Mathf.Abs(spawnPosition.x) > limit.x / 2
                || Mathf.Abs(spawnPosition.y) > limit.y / 2
                ||
                Vector2.Distance(transform.position, spawnPosition) > baseRange
               )
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
            if (Settings != null)
                Gizmos.DrawWireCube(transform.position, Settings.EnemySpwanLimitation);
        }
    }
}
