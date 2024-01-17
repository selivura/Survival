using Cinemachine;
using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private float _intensity = 5;
        CinemachineVirtualCamera _vcam;
        private Timer _timer = new Timer(0, 0);
        private CinemachineBasicMultiChannelPerlin _noise;
        private void Awake()
        {
            _vcam = GetComponent<CinemachineVirtualCamera>();
            _noise = _vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        private void FixedUpdate()
        {
            if (_timer.Expired)
            {
                _noise.m_AmplitudeGain = 0;
            }
        }
        public void Shake(float time)
        {
            _noise.m_AmplitudeGain = _intensity;
            _timer = new Timer(time, Time.time);
        }
    }
}
