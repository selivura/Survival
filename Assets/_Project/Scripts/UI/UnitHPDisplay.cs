using UnityEngine;

namespace Selivura.UI
{
    public class UnitHPDisplay : BarUIDisplay
    {
        [SerializeField] Unit _unit;
        private void OnEnable()
        {
            if (_unit == null)
            {
                Debug.LogError("No Unit assigned!");
                return;
            }
            _unit.OnHealthChanged.AddListener(OnHealthChanged);
            OnHealthChanged(_unit);
        }
        private void OnDisable()
        {
            _unit.OnHealthChanged.RemoveListener(OnHealthChanged);
        }
        private void OnHealthChanged(Unit unit)
        {
            progressBar.CurrentValue = unit.CurrentHealth;
            progressBar.Max = unit.MaxHealth;
            progressBar.Min = 0;
        }
    }
}
