using UnityEngine;

namespace Selivura
{
    public enum SoundChannel
    {
        SFX,
        BGM,
    }
    public class AudioPlayer : Singleton<AudioPlayer>, IDependecyProvider
    {
        [SerializeField] AudioSource _sfxAudioSource;
        [SerializeField] AudioSource _bgmAudioSource;

        public static void PlaySound(AudioClip clip, SoundParameters parameters)
        {
            switch (parameters.Channel)
            {
                case SoundChannel.SFX:
                    Current._sfxAudioSource.PlayOneShotWithParameters(clip, parameters);
                    break;
                case SoundChannel.BGM:
                    Current._bgmAudioSource.PlayOneShotWithParameters(clip, parameters);
                    break;
                default:
                    //Impossible... hopefully
                    break;
            }
        }
    }
    public class SoundParameters
    {
        public SoundChannel Channel = SoundChannel.SFX;
        public float Volume = 1;
        public float MinPitch = 1;
        public float MaxPitch = 1;
        public SoundParameters(SoundChannel channel, float volume, float minPitch, float maxPitch)
        {
            Channel = channel;
            Volume = volume;
            MinPitch = minPitch;
            MaxPitch = maxPitch;
        }
        public class Builder
        {
            SoundChannel _channel;
            float _volume = 1;
            float _minPitch = 0.95f;
            float _maxPitch = 1.05f;
            public Builder WithChannel(SoundChannel channel)
            {
                _channel = channel;
                return this;
            }

            public Builder WithVolume(float volume)
            {
                _volume = volume;
                return this;
            }
            public Builder WithMinPitch(float minPitch)
            {
                _minPitch = minPitch;
                return this;
            }
            public Builder WithMaxPitch(float maxPitch)
            {
                _maxPitch = maxPitch;
                return this;
            }
            public SoundParameters Build()
            {
                return new SoundParameters(_channel, _volume, _minPitch, _maxPitch);
            }
        }
    }



}
