using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Selivura.Player
{
    public class PlayerUnit : FriendlyUnit, IDependecyProvider
    {
        public bool CombatEnabled => EnergyLeft > 0;
        public int MatterHarvested { get; private set; } = 0;
        public float EnergyLeft = 100;
        public float EnergyMax => Energy.Value;

        public PlayerStat Energy = new PlayerStat();
        public PlayerStat EnergyRegeneration = new PlayerStat();
        public PlayerStat EnergyDecay = new PlayerStat();
        public PlayerStat MovementSpeed = new PlayerStat();
        public PlayerStat AttackDamage = new PlayerStat();
        public PlayerStat AttackCooldown = new PlayerStat();
        public PlayerStat ProjectileSpeed = new PlayerStat();
        public PlayerStat AttackRange = new PlayerStat();
        public List<Item> Inventory { get; private set; } = new List<Item>();

        public UnityEvent<float> OnMatterChanged;
        public UnityEvent<float> OnMatterIncreased;
        public UnityEvent<float> OnMatterDecreased;
        public delegate void EnergyChangeDelegate();
        public event EnergyChangeDelegate OnEnergyChanged;

        private Timer _energyDecayTiemr = new Timer(0, 0);
        [SerializeField] private float _energyDecayCooldown = .1f;
        public bool InfiniteEnergy = false;
        [Provide]
        public PlayerUnit Provide()
        {
            return this;
        }
        private void FixedUpdate()
        {
            if (InfiniteEnergy)
                return;
            if (_energyDecayTiemr.Expired)
            {
                _energyDecayTiemr = new Timer(_energyDecayCooldown, Time.time);
                ChangeEnergy(-EnergyDecay.Value * _energyDecayCooldown);
            }
        }
        public override void Initialize(int additiveHealth = 0)
        {
            base.Initialize(additiveHealth);
            EnergyLeft = EnergyMax;
        }
        public void AddItem(Item itemPrefab)
        {
            Inventory.Add(itemPrefab);
            var spawned = Instantiate(itemPrefab, transform);
            spawned.OnPickup(this);
        }
        public void GiveAllMatterToBase(MainBase mainBase)
        {
            mainBase.ChangeMatter(MatterHarvested);
            MatterHarvested = 0;
        }
        public void ChangeMatter(int matter)
        {
            MatterHarvested += matter;
            OnMatterChanged?.Invoke(matter);
            if (matter > 0)
                OnMatterIncreased?.Invoke(matter);
            else
                OnMatterDecreased?.Invoke(matter);
        }

        public void ChangeEnergy(float value)
        {
            EnergyLeft += value;
            if (EnergyLeft > EnergyMax)
                EnergyLeft = EnergyMax;
            if (EnergyLeft <= 0)
            {
                EnergyLeft = 0;
                ChangeHealth(-Mathf.RoundToInt(EnergyDecay.Value));
            }
            OnEnergyChanged?.Invoke();
        }
    }
}
