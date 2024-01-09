using Selivura.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public class MainBase : FriendlyBase, IInteractable, IDependecyProvider
    {
        public int Matter = 0;
        public int Level = 1;
        public float RegenerationPercent = .1f;
        public int MatterToLevelUp = 5;
        public MainBaseData BaseData;
        public delegate void OnBaseStatChangedDelegate();
        public event OnBaseStatChangedDelegate OnMatterChanged;
        public EnergyRegenArea CombatArea;

        public UnityEvent OnLevelUp;
        [SerializeField] private const int _playerHealing = 30;
        [SerializeField] private const int _requiredMatterToHeal = 5;

        [Provide]
        public MainBase Provide()
        {
            return this;
        }
        private void OnValidate()
        {
            if (!CombatArea)
                Debug.LogWarning("No combat area assigned!");
        }
        private void Start()
        {
            Initialize();
            _maxHealth = BaseData.BaseHealth;
            _currentHealth = _maxHealth;
            CombatArea.Radius = BaseData.BaseCombatRadius;
        }
        public void ChangeMatter(int materiaAmount)
        {
            Matter += materiaAmount;
            if (Matter >= MatterToLevelUp)
            {
                LevelUp();
            }
            OnMatterChanged?.Invoke();
        }
        private void LevelUp()
        {
            Matter -= MatterToLevelUp;
            MatterToLevelUp += BaseData.MatterProgression;
            _maxHealth += BaseData.HealthPerLevel;
            ChangeHealth(BaseData.HealthPerLevel + Mathf.RoundToInt(_maxHealth * (RegenerationPercent / 100f)));
            CombatArea.Radius += BaseData.CombatRadiusPerLevel;

            Level++;
            OnLevelUp?.Invoke();
            //it should never happen btw
            if (Matter < 0)
                Matter = 0;
        }
        public void Interact(PlayerUnit interactor)
        {
            if (!CanInteract(interactor))
                return;
            int claim = Mathf.Min(interactor.MatterHarvested, MatterToLevelUp);
            interactor.ChangeMatter(-claim);
            if (interactor.MatterHarvested >= _requiredMatterToHeal)
                interactor.ChangeHealth(claim / _requiredMatterToHeal * _playerHealing);
            ChangeMatter(claim);
        }
        public bool CanInteract(PlayerUnit interactor)
        {
            return interactor.MatterHarvested >= 0;
        }
    }
}
