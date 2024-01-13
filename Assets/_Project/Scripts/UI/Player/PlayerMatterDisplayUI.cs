using Selivura.Player;

namespace Selivura.UI
{
    public class PlayerMatterDisplayUI : TextDisplayUI
    {
        [Inject]
        PlayerUnit _playerUnit;
        private void OnEnable()
        {
             FindFirstObjectByType<Injector>().Inject(this);
            _playerUnit.OnMatterChanged.AddListener(OnMatterChanged);
            OnMatterChanged();
        }
        private void OnDisable()
        {
            _playerUnit.OnMatterChanged.RemoveListener(OnMatterChanged);
        }
        private void OnMatterChanged(float amount = 0)
        {
            tmpText.text = prefix + _playerUnit.MatterHarvested;
        }
    }
}
