using Selivura.Player;
using UnityEngine;

namespace Selivura.UI
{
    public class PlayerEnergyDisplay : BarUIDisplay
    {
        [SerializeField] PlayerUnit _player;
        private void OnEnable()
        {
            _player.OnEnergyChanged += UpdateText;
        }
        private void OnDisable()
        {
            _player.OnEnergyChanged -= UpdateText;
        }
        private void UpdateText()
        {
            progressBar.CurrentValue = _player.EnergyLeft;
            progressBar.Min = 0;
            progressBar.Max = _player.Energy.Value;
        }
    }
}
