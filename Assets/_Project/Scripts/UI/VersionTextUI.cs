using UnityEngine;

namespace Selivura.UI
{
    public class VersionTextUI : TextDisplayUI
    {
        private void OnEnable()
        {
            tmpText.text = prefix + Application.version;
        }
    }
}
