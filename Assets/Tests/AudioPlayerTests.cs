using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Selivura.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Selivura.Tests
{
    public class AudioPlayerTests
    {
        [UnityTest]
        public IEnumerator AudioPlayerGlobalVolumeTest()
        {
            yield return SceneManager.LoadSceneAsync(0);
            AudioPlayer audioPlayer = GameObject.FindAnyObjectByType<AudioPlayer>();
            yield return new WaitForFixedUpdate();
            float volumeBeforeTest = AudioListener.volume;
            audioPlayer.SetGlobalVolume(0.123f);
            Assert.AreEqual(0.123f, AudioListener.volume);

            audioPlayer.SetGlobalVolume(volumeBeforeTest);
            Assert.AreEqual(volumeBeforeTest, AudioListener.volume);
        }
        [UnityTest]
        public IEnumerator AudioPlayerSFXVolumeTest()
        {
            yield return SceneManager.LoadSceneAsync(0);
            AudioPlayer audioPlayer = GameObject.FindAnyObjectByType<AudioPlayer>();
            yield return new WaitForFixedUpdate();
            float volumeBeforeTest = audioPlayer.GetChannelVolume(SoundChannel.SFX);

            audioPlayer.SetChannelVolume(0.123f, SoundChannel.SFX);
            Assert.AreEqual(0.123f, audioPlayer.GetChannelVolume(SoundChannel.SFX));

            audioPlayer.SetChannelVolume(volumeBeforeTest, SoundChannel.SFX);
            Assert.AreEqual(volumeBeforeTest, AudioListener.volume);
        }

        [UnityTest]
        public IEnumerator AudioPlayerBGMVolumeTest()
        {
            yield return SceneManager.LoadSceneAsync(0);
            AudioPlayer audioPlayer = GameObject.FindAnyObjectByType<AudioPlayer>();
            yield return new WaitForFixedUpdate();
            float volumeBeforeTest = audioPlayer.GetChannelVolume(SoundChannel.BGM);

            audioPlayer.SetChannelVolume(0.123f, SoundChannel.BGM);
            Assert.AreEqual(0.123f, audioPlayer.GetChannelVolume(SoundChannel.BGM));

            audioPlayer.SetChannelVolume(volumeBeforeTest, SoundChannel.BGM);
            Assert.AreEqual(volumeBeforeTest, AudioListener.volume);
        }
    }
}
