using UnityEngine;

namespace Selivura.UI
{
    public class PositionLocker : MonoBehaviour
    {
        Vector3 _positionLock;
        public void Initialize(Vector3 position)
        {
            _positionLock = position;
        }
        private void Update()
        {
            transform.position = _positionLock;
        }
    }
}
