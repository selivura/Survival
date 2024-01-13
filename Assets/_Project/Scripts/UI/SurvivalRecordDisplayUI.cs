using UnityEngine;

namespace Selivura.UI
{
    public class SurvivalRecordDisplayUI : TextDisplayUI
    {
        [SerializeField] RecordSaver _recordSaver;
        private void OnValidate()
        {
            if (!_recordSaver)
            {
                Debug.LogError($"{gameObject} has no {_recordSaver.GetType()} assigned");
            }
        }
        public void UpdateText()
        {
            tmpText.text =
                prefix
                + "\nWaves: " + _recordSaver.CurrentWaves
                + "\nLoops: " + _recordSaver.CurrentLoops
                + "\nRecord:"
                + "\nWaves: " + _recordSaver.RecordWaves
                + "\nLoops: " + _recordSaver.RecordLoops;
        }
        private void OnEnable()
        {
            _recordSaver.OnRecordUpdated.AddListener(UpdateText);
            UpdateText();
        }
        private void OnDisable()
        {
            _recordSaver.OnRecordUpdated.RemoveListener(UpdateText);
        }
    }
}
