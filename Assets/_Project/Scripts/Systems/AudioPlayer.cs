using System;
using UnityEngine;

namespace Selivura
{
    public enum SoundChannel
    {
        SFX,
        BGM,
    }
    public class AudioPlayer : MonoBehaviour, IDependecyProvider
    {
        AudioSource _sfxAudioSource;
        AudioSource _bgmAudioSource;
        private const string _globalVolumePlayerPrefsKey = "Global_Volume";
        private const string _sfxVolumePlayerPrefsKey = "SFX_Volume";
        private const string _bgmVolumePlayerPrefsKey = "BGM_Volume";

        public float SFXVolume { get; private set; }
        public float BGMVolume { get; private set; }
        [Provide]
        public AudioPlayer Provide()
        {
            return this;
        }
        protected void Awake()
        {
            _sfxAudioSource = CreateAudioSource("SFX");
            _bgmAudioSource = CreateAudioSource("BGM");

            AudioListener.volume = PlayerPrefs.GetFloat(_globalVolumePlayerPrefsKey, 0.75f);
            SFXVolume = PlayerPrefs.GetFloat(_sfxVolumePlayerPrefsKey, 1);
            BGMVolume = PlayerPrefs.GetFloat(_bgmVolumePlayerPrefsKey, 1);
        }

        private AudioSource CreateAudioSource(string name)
        {
            GameObject sfxObject = new GameObject(name);
            sfxObject.transform.SetParent(transform, false);
            return sfxObject.AddComponent<AudioSource>();
        }

        private void SaveAllCurrentParameters()
        {
            PlayerPrefs.SetFloat(_globalVolumePlayerPrefsKey, AudioListener.volume);
            PlayerPrefs.SetFloat(_sfxVolumePlayerPrefsKey, SFXVolume);
            PlayerPrefs.SetFloat(_bgmVolumePlayerPrefsKey, BGMVolume);
        }
        public void SetGlobalVolume(float value)
        {
            AudioListener.volume = value;
            SaveAllCurrentParameters();
        }
        public void SetChannelVolume(float volume, SoundChannel channel)
        {
            switch (channel)
            {
                case SoundChannel.SFX:
                    SFXVolume = volume;
                    break;
                case SoundChannel.BGM:
                    BGMVolume = volume;
                    break;
                default:
                    SFXVolume = volume;
                    break;
            }
           SaveAllCurrentParameters();
        }
        public float GetChannelVolume(SoundChannel channel)
        {
            switch (channel)
            {
                case SoundChannel.SFX:
                    return SFXVolume;
                case SoundChannel.BGM:
                    return BGMVolume;
                default:
                    return AudioListener.volume;
            }
        }
        public void PlaySound(AudioClip clip, SoundParameters parameters)
        {
            switch (parameters.Channel)
            {
                case SoundChannel.SFX:
                    _sfxAudioSource.PlayOneShotWithParameters(clip, parameters);
                    break;
                case SoundChannel.BGM:
                    _bgmAudioSource.PlayOneShotWithParameters(clip, parameters);
                    break;
                default:
                    _sfxAudioSource.PlayOneShotWithParameters(clip, parameters);
                    break;
            }
        }

        internal void PlaySound(object shootSound, SoundParameters soundParameters)
        {
            throw new NotImplementedException();
        }
    }
    [Serializable]
    public class SoundParameters
    {
        public SoundChannel Channel = SoundChannel.SFX;
        public float Volume = 1;
        public float MinPitch = 1;
        public float MaxPitch = 1;
        public SoundParameters(SoundChannel channel, float volume, float minPitch, float maxPitch)
        {
            Channel = channel;
            Volume = Mathf.Clamp01(volume);
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
