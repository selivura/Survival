using UnityEngine;
using UnityEngine.UI;

namespace Selivura.UI
{
    public class IconBlinkWhenLow : BarUIDisplay
    {
        [SerializeField] Image _icon;
        [SerializeField] Color _normalColor = Color.white;
        [SerializeField] Color _blinkColor = Color.clear;
        [SerializeField] float _blinkThreshold = 0.33f;
        [SerializeField] float _blinkEvery = 0.1f;
        Timer _blinkTimer = new Timer(0, 0);
        private void FixedUpdate()
        {
            if (progressBar.CurrentValue > progressBar.Max * _blinkThreshold)
            {
                _icon.color = _normalColor;
                return;
            }
            if (_blinkTimer.Expired)
                Blink();

        }
        private void Blink()
        {
            _blinkTimer = new Timer(_blinkEvery, Time.time);
            if (_icon.color == _normalColor)
                _icon.color = _blinkColor;
            else
                _icon.color = _normalColor;
        }
    }
}
