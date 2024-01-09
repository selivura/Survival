using UnityEngine;

namespace Selivura.UI
{
    public class UnitHPFloatingBar : BarUIDisplay
    {
        public Unit Unit;
        public Transform LockTransform;
        public void Initialize()
        {
            Unit.OnHealthChanged.AddListener(UpdateBar);
            UpdateBar(Unit);
        }
        private void OnDestroy()
        {
            Unit.OnHealthChanged.RemoveListener(UpdateBar);
        }
        private void UpdateBar(Unit unit)
        {
            progressBar.Min = 0;
            progressBar.Max = unit.MaxHealth;
            progressBar.CurrentValue = unit.CurrentHealth;
        }
        private void Update()
        {
            transform.position = LockTransform.position;
        }
    }
}
