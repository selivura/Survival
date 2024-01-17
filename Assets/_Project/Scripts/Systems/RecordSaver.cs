using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    [RequireComponent(typeof(EnemyWaveController))]
    public class RecordSaver : MonoBehaviour, IDependecyProvider
    {
        private const string _waveRecodKey = "WavesRecord";
        private const string _loopRecodKey = "LoopRecord";
        EnemyWaveController _waveController;
        [Inject]
        private GameStateController _gameStateController;
        public int RecordWaves { get; private set; }
        public int RecordLoops { get; private set; }
        public int CurrentWaves { get; private set; }
        public int CurrentLoops { get; private set; }
        public UnityEvent OnRecordUpdated;

        [Provide]
        private RecordSaver Provide() => this;

        private void Awake()
        {
            _waveController = GetComponent<EnemyWaveController>();
            _waveController.OnWaveStarted.AddListener(UpdateRecord);
            _gameStateController.OnGameOver.AddListener(SaveRecord);
            LoadRecord();
        }
        private void OnDestroy()
        {
            _gameStateController.OnGameOver.RemoveListener(SaveRecord);
            _waveController.OnWaveStarted.RemoveListener(UpdateRecord);
        }
        private void UpdateRecord()
        {
            CurrentWaves = _waveController.TotalWaves;
            CurrentLoops = _waveController.Loop;
            OnRecordUpdated?.Invoke();
        }
        private void LoadRecord()
        {
            RecordWaves = PlayerPrefs.GetInt(_waveRecodKey);
            RecordLoops = PlayerPrefs.GetInt(_loopRecodKey);
            OnRecordUpdated?.Invoke();
        }
        private void SaveRecord()
        {
            LoadRecord();
            if (CurrentLoops > RecordLoops)
            {
                RecordLoops = CurrentLoops;
            }
            if (CurrentWaves > RecordWaves)
            {
                RecordWaves = CurrentWaves;
            }
            PlayerPrefs.SetInt(_waveRecodKey, RecordWaves);
            PlayerPrefs.SetInt(_loopRecodKey, RecordLoops);
            OnRecordUpdated?.Invoke();
        }
    }
}
