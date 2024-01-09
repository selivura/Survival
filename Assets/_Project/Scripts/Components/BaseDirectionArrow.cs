using UnityEngine;

namespace Selivura
{
    public class BaseDirectionArrow : MonoBehaviour
    {
        MainBase _base;
        private void OnEnable()
        {
            _base = FindAnyObjectByType<MainBase>();
        }
        private void FixedUpdate()
        {
            transform.right = _base.transform.position - transform.position;
        }
    }
}
