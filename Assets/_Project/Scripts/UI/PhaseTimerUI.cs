using System;

namespace Selivura.UI
{
    public class PhaseTimerUI : TextDisplayUI
    {
        EnemyWaveController _waveController;
        private void OnEnable()
        {
            _waveController = FindAnyObjectByType<EnemyWaveController>();
        }
        private void FixedUpdate()
        {
            if (_waveController.CurrentPhaseType == PhaseType.Peace)
            {
                var time = TimeSpan.FromSeconds(_waveController.PhaseTimeLeft);
                tmpText.text = "Next wave in: \n" + time.ToString(@"mm\:ss\:ff");
            }
            else
            {
                tmpText.text = "Wave " + _waveController.CurrentWaveIndex + 1;
            }
        }
    }
}
