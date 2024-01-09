using System;
using UnityEngine;

namespace Selivura.UI
{
    public class SurvivalTimeDisplayUI : TextDisplayUI
    {
        [SerializeField] EnemyWaveController _enemyWaveController;
        private void OnEnable()
        {
            if (!_enemyWaveController)
            {
                Debug.LogError("No EnemyWaveController assigned");
                return;
            }
            tmpText.text = prefix + TimeSpan.FromSeconds(_enemyWaveController.TotalSurvivalTime).ToString(@"hh\:mm\:ss") + "\nWaves: " + _enemyWaveController.CurrentWave;
        }
    }
}
