using UnityEngine;

namespace Selivura
{
    public class RandomSoundEffectPlayer : MonoBehaviour
    {
        [SerializeField] AudioClip[] _sounds;
        [SerializeField] float _volume = 0.5f;
        [SerializeField] float _randomPitchMax = 1.05f;
        [SerializeField] float _randomPitchMin = 0.95f;
        [SerializeField] SoundChannel _channel = SoundChannel.SFX;

        [Inject]
        AudioPlayer _audioPlayer;

        private void Awake()
        {
            Injector.Instance.Inject(this);
        }
        public void PlayRandomSound()
        {
            SoundParameters parameters = new SoundParameters.Builder()
                .WithChannel(_channel)
                .WithMaxPitch(_randomPitchMax)
                .WithMinPitch(_randomPitchMin)
                .WithVolume(_volume)
                .Build();
            _audioPlayer.PlaySound(_sounds.GetRandomElement(), parameters);
        }
    }
}
