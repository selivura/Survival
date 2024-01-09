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
        public void PlayRandomSound()
        {
            SoundParameters parameters = new SoundParameters.Builder()
                .WithChannel(_channel)
                .WithMaxPitch(_randomPitchMax)
                .WithMinPitch(_randomPitchMin)
                .WithVolume(_volume)
                .Build();
            AudioPlayer.PlaySound(_sounds.GetRandomElement(), parameters);
        }
    }
}
