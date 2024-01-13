using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura.UI
{
    [ExecuteInEditMode]
    public class LinearProgressBar : ProgressBar
    {
#if UNITY_EDITOR
        [MenuItem("GameObject/UI/Linear Progress bar")]
        public static void AddLinearProgressBar()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("UI/LinearProgressBar"));
            if (Selection.activeGameObject)
                obj.transform.SetParent(Selection.activeGameObject.transform, false);
        }
#endif
        public Image Mask;
        public Image fill;
        protected virtual void FixedUpdate()
        {
            GetCurrentFill();
        }
        public override float GetCurrentFill()
        {
            float currentOffset = CurrentValue - Min;
            float maxOffset = Max - Min;
            if (maxOffset == 0)
                return 0;
            float fillAmount = currentOffset / maxOffset;
            Mask.fillAmount = fillAmount;
            return fillAmount;
        }
    }
}