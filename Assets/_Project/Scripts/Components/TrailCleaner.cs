using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailCleaner : MonoBehaviour
    {
        private TrailRenderer _trailRenderer;
        private void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
        }
        void OnDisable()
        {
            _trailRenderer.Clear();
        }
    }
}
