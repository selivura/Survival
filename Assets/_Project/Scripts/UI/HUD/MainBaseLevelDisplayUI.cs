using UnityEngine;

namespace Selivura.UI
{
    public class MainBaseLevelDisplayUI : TextDisplayUI
    {
        [SerializeField] private MainBase _base;
        private void OnEnable()
        {
            _base.OnMatterChanged += OnMatterChanged;
            OnMatterChanged();
        }
        private void OnDisable()
        {
            _base.OnMatterChanged -= OnMatterChanged;
        }
        private void OnMatterChanged()
        {
            tmpText.text = $"LV.{_base.Level} ({_base.Matter}/{_base.XPToLevelUp})";
        }
    }
}
