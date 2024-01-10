using UnityEngine;

namespace Selivura
{
    public class AreaRangeDisplayer : MonoBehaviour
    {
        public InfiniteEnergyArea Area;
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
                transform.localScale = new Vector3(Area.Radius * 2, Area.Radius * 2, 1);
        }
    }
}
