using Selivura.ObjectPooling;
using System;
using System.Collections.Generic;
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
        private const string _globalVolumePlayerPrefsKey = "Global_Volume";
        private const string _channelVolumePrefix = "ChannelVolume_";
        private ObjectPool<AudioInstance> _audioPool;
        public List<float> Volumes { get; private set; } = new List<float>();
        [Provide]
        public AudioPlayer Provide()
        {
            return this;
        }
        protected void Awake()
        {
            _audioPool = new ObjectPool<AudioInstance>(new GameObject("AudioInstance").AddComponent<AudioInstance>(), 50, transform);

            AudioListener.volume = PlayerPrefs.GetFloat(_globalVolumePlayerPrefsKey, 0.75f);
            for (int i = 0; i < Enum.GetValues(typeof(SoundChannel)).Length; i++)
            {
                Volumes.Add(PlayerPrefs.GetFloat(_channelVolumePrefix + i.ToString(), 1));
            }
        }

        private void SaveAllCurrentParameters()
        {
            PlayerPrefs.SetFloat(_globalVolumePlayerPrefsKey, AudioListener.volume);
            for (int i = 0; i < Enum.GetValues(typeof(SoundChannel)).Length; i++)
            {
                PlayerPrefs.SetFloat(_channelVolumePrefix + i.ToString(), Volumes[i]);
            }
        }
        public void SetGlobalVolume(float value)
        {
            AudioListener.volume = value;
            SaveAllCurrentParameters();
        }
        public void SetChannelVolume(float volume, SoundChannel channel)
        {
            Volumes[(int)channel] = volume;
            SaveAllCurrentParameters();
        }
        public float GetChannelVolume(SoundChannel channel)
        {
            return Volumes[(int)channel];
        }
        public void PlaySound(AudioClip clip, SoundParameters parameters)
        {
            var instance = _audioPool.GetFreeElement();
            instance.SetPitch(UnityEngine.Random.Range(parameters.MinPitch, parameters.MaxPitch));
            instance.SetVolume(parameters.Volume * GetChannelVolume(parameters.Channel));
            instance.Play(clip);
        }
    }
    [System.Serializable]
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
