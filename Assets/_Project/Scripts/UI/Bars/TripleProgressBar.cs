using UnityEngine;
namespace Selivura.UI
{
    [ExecuteInEditMode]
    public class TripleProgressBar : ProgressBar
    {
        public float MaxDelta = 1;
        //float timer = 0;
        //float _lastCurrent;
        [SerializeField] LinearProgressBar _damageBar;
        [SerializeField] LinearProgressBar _healBar;
        [SerializeField] LinearProgressBar _mainBar;
        //[SerializeField] ProgressBar _overheal;
        protected void FixedUpdate()
        {
            _healBar.CurrentValue = CurrentValue;
            _healBar.Max = Max;
            _healBar.Min = Min;
            GetCurrentFill();
        }
        public override float GetCurrentFill()
        {
            _damageBar.Max = _healBar.Max;
            _mainBar.Max = _healBar.Max;
            _damageBar.Min = _healBar.Min;
            _mainBar.Min = _healBar.Min;
            // _overheal.Max = _healBar.Max * 2;
            // _overheal.Min = _healBar.Max;

            //_overheal.CurrentValue = _healBar.CurrentValue;
            // _overheal.CurrentValue = Mathf.Clamp(_overheal.CurrentValue, _healBar.Max, _healBar.Max * 2);

            _damageBar.CurrentValue = Mathf.Clamp(_damageBar.CurrentValue, _healBar.CurrentValue, _healBar.Max);
            _damageBar.CurrentValue = CalculateDelta(_damageBar.CurrentValue, _healBar.CurrentValue);

            _mainBar.CurrentValue = Mathf.Clamp(_mainBar.CurrentValue, 0, _healBar.CurrentValue);
            _mainBar.CurrentValue = CalculateDelta(_mainBar.CurrentValue, _healBar.CurrentValue);
            return _mainBar.CurrentValue;
        }
        private float CalculateDelta(float current, float target)
        {
            return Mathf.MoveTowards(current, target, Time.fixedDeltaTime * MaxDelta * _healBar.Max / 100);
        }
    }
}