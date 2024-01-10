using UnityEngine;
using UnityEngine.UI;

namespace Selivura.UI
{
    [RequireComponent(typeof(Slider))]
    public class VolumeSlider : MonoBehaviour
    {
        public SoundChannel Channel;
        [SerializeField] private bool _global;
        private Slider slider;

        [Inject]
        private AudioPlayer _audioPlayer;
        private void Awake()
        {
            Injector.Instance.Inject(this);
        }
        private void Start()
        {
            slider =  GetComponent<Slider>();
            if (_global)
            {
                slider.value = AudioListener.volume;
                slider.onValueChanged.AddListener(GlobalVolumeSlider);
            }
            else
            {
                slider.value = _audioPlayer.GetChannelVolume(Channel);
                slider.onValueChanged.AddListener(ChannelSlider);
            }
        }
        private void OnDestroy()
        {
            if (_global)
            {
                slider.onValueChanged.RemoveListener(ChannelSlider);
            }
            else
            {
                slider.onValueChanged.RemoveListener(GlobalVolumeSlider);
            }
        }
        public void GlobalVolumeSlider(float value)
        {
            _audioPlayer.SetGlobalVolume(value);
        }
        public void ChannelSlider(float value)
        {
            _audioPlayer.SetChannelVolume(value, Channel);
        }
    }
}
