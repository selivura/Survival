using Selivura.Player;
using UnityEngine;
using UnityEngine.Rendering;

namespace Selivura.PostProcessing
{
    [RequireComponent(typeof(Volume))]
    public class PlayerLowEnergyPostProcessing : MonoBehaviour
    {
        private Volume _volume;
        [SerializeField] private PlayerUnit _player;
        [SerializeField] private float _playerEnergyThreshold = 0.3f;
        [SerializeField] private float _defaultVolumeWeight = 1;
        private void Awake()
        {
            _volume = GetComponent<Volume>();
        }
        private void OnEnable()
        {
            _player.OnEnergyChanged += UpdateVolume;
        }
        private void OnDisable()
        {
            _player.OnEnergyChanged -= UpdateVolume;
        }
        private void UpdateVolume()
        {
            var value = _player.EnergyLeft / _player.MaxEnergy;
            if (value > _playerEnergyThreshold)
                _volume.weight = _defaultVolumeWeight;
            else
            {
                _volume.weight = 1 - Mathf.Clamp(value * (1 / _playerEnergyThreshold), 0, 1);
            }
        }
    }
}
