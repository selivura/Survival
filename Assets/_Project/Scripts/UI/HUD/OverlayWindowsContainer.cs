using UnityEngine;

namespace Selivura.UI
{
    public class OverlayWindowsContainer : MonoBehaviour, IDependecyProvider
    {
        [Provide]
        public OverlayWindowsContainer Provide()
        {
            return this;
        }
    }
}
