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
            AudioPlayer audioPlayer = TestUtils.CreateAudioPlayer();
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
            yield return ChannelTest(SoundChannel.SFX);
        }
       
        [UnityTest]
        public IEnumerator AudioPlayerBGMVolumeTest()
        {
            yield return ChannelTest(SoundChannel.BGM);
        }
        public IEnumerator ChannelTest(SoundChannel channel)
        {
            AudioPlayer audioPlayer = TestUtils.CreateAudioPlayer();
            yield return new WaitForFixedUpdate();
            float volumeBeforeTest = audioPlayer.GetChannelVolume(channel);

            audioPlayer.SetChannelVolume(0.123f, channel);
            Assert.AreEqual(0.123f, audioPlayer.GetChannelVolume(channel));

            audioPlayer.SetChannelVolume(volumeBeforeTest, channel);
            Assert.AreEqual(volumeBeforeTest, audioPlayer.GetChannelVolume(channel));
        }

       
    }
}
