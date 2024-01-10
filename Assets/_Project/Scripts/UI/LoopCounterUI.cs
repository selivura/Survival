using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura.UI
{
    public class LoopCounterUI : TextDisplayUI
    {
        [Inject]
        EnemyWaveController _waveController;
        [SerializeField] GameObject _contaienr;

        private void OnEnable()
        {
            _waveController.OnWaveStarted.AddListener(UpdateText);
            UpdateText();
        }
        private void OnDisable()
        {
            _waveController.OnWaveStarted.RemoveListener(UpdateText);
        }
        private void UpdateText()
        {
            int loop = _waveController.Loop;
            if (loop > 0)
                _contaienr.SetActive(true);
            else
                _contaienr.SetActive(false);
            tmpText.text = prefix + loop;
        }
    }
}
