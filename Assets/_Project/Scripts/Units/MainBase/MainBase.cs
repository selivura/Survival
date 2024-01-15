using Selivura.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public class MainBase : Unit, IInteractable, IDependecyProvider
    {
        public int Matter = 0;
        public int Level = 1;
        public int XPToLevelUp = 5;
        public MainBaseData BaseData;
        public delegate void OnBaseStatChangedDelegate();
        public event OnBaseStatChangedDelegate OnMatterChanged;
        public Area Area;

        public UnityEvent OnLevelUp;

        [Provide]
        public MainBase Provide()
        {
            return this;
        }
        private void Start()
        {
            Initialize();
            if (!BaseData)
            {
                Debug.LogWarning($"No base data was assigned for {gameObject.name}, creating new one");
                BaseData = new MainBaseData();
            }
            if(!Area)
            {
                Debug.LogWarning($"No {Area.GetType()} was assigned for {gameObject.name}, creating new one");
                GameObject areaObject = new GameObject();
                Area = areaObject.AddComponent<InfiniteEnergyArea>();
            }
            _maxHealth = BaseData.BaseHealth;
            _currentHealth = _maxHealth;
            Area.Radius = BaseData.BaseEnergyRegenRadius;
        }
        public void ChangeMatter(int materiaAmount)
        {
            Matter += materiaAmount;
            if (Matter >= XPToLevelUp)
            {
                LevelUp();
            }
            OnMatterChanged?.Invoke();
        }
        private void LevelUp()
        {
            Matter -= XPToLevelUp;
            XPToLevelUp += BaseData.XPProgression;
            _maxHealth += BaseData.HealthPerLevel;
            ChangeHealth(BaseData.HealthPerLevel + Mathf.RoundToInt(_maxHealth * (BaseData.RegenerationPercent / 100f)));
            Area.Radius += BaseData.EnergyRegenRadiusPerLevel;

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
            int claim = Mathf.Min(interactor.MatterHarvested, XPToLevelUp);
            interactor.ChangeMatter(-claim);
            ChangeMatter(claim);
        }
        public bool CanInteract(PlayerUnit interactor)
        {
            return interactor.MatterHarvested >= XPToLevelUp;
        }
    }
}
