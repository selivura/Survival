using UnityEngine;

namespace Selivura.UI
{
    public abstract class TextDisplayUI : MonoBehaviour
    {
        [SerializeField] protected TMPro.TMP_Text tmpText;
        [SerializeField] protected string prefix = "";
    }
}
