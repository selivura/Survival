using UnityEngine;

namespace Selivura
{
    public class DisableIfweb : MonoBehaviour
    {
#if PLATFORM_WEBGL
        private void Awake()
        {
            gameObject.SetActive(false);
        }
#endif
    }
}
