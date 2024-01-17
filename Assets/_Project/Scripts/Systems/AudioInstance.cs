using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioInstance : MonoBehaviour
    {
        AudioSource _audioSource;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        public void SetVolume(float volume)
        {
            _audioSource.volume = volume;
        }
        public void SetPitch(float pitch)
        {
            _audioSource.pitch = pitch;
        }
        public void Play(AudioClip clip)
        {
            CancelInvoke();
            _audioSource.clip = clip;
            _audioSource.Play();
            Invoke(nameof(Deactivate), clip.length + 0.1f);
        }
        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
