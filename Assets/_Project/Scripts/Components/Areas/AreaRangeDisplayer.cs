using UnityEngine;

namespace Selivura
{
    public class AreaRangeDisplayer : MonoBehaviour
    {
        public Area Area;
        private void OnValidate()
        {
            OnCombatRadiusChanged();
        }
        private void OnEnable()
        {
            Area.OnAreaChanged.AddListener(OnCombatRadiusChanged);
        }
        private void OnDisable()
        {
            Area.OnAreaChanged.AddListener(OnCombatRadiusChanged);
        }
        private void OnCombatRadiusChanged()
        {
            if (Area != null)
                transform.localScale = new Vector3(Area.Radius * 2, Area.Radius * 2, 1);
        }
    }
}
