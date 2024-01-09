using UnityEngine;

namespace Selivura
{
    public class BaseCombatRangeDisplayer : MonoBehaviour
    {
        public CombatArea Area;
        private void OnValidate()
        {
            OnCombatRadiusChanged();
        }
        private void OnEnable()
        {
            Area.OnAreaChanged += OnCombatRadiusChanged;
        }
        private void OnDisable()
        {
            Area.OnAreaChanged -= OnCombatRadiusChanged;
        }
        private void OnCombatRadiusChanged()
        {
            if (Area != null)
                transform.localScale = new Vector3(Area.CombatEnableRadius * 2, Area.CombatEnableRadius * 2, 1);
        }
    }
}
