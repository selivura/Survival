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
            if (_waveController.CurrentPhase == PhaseType.Peace)
            {
                var time = TimeSpan.FromSeconds(_waveController.PeacePhaseTimeLeft);
                tmpText.text = "Next wave in: \n" + time.ToString(@"mm\:ss\:ff");
            }
            else
            {
                tmpText.text = "Wave " + _waveController.CurrentWave;
            }
        }
    }
}
