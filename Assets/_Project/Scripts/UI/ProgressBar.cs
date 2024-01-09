using UnityEngine;

namespace Selivura.UI
{
    public abstract class ProgressBar : MonoBehaviour
    {
        public float Max;
        public float Min;
        public float CurrentValue;
        public abstract float GetCurrentFill();
    }
}
