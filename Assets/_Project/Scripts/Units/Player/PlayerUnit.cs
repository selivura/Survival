using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Selivura.Player
{
    public class PlayerUnit : Unit, IDependecyProvider
    {
        public float EnergyLeft { get; private set; }
        public bool InfiniteEnergy = false;
        public float MaxEnergy => PlayerStats.Energy.Value;

        private Timer _energyDecayTiemr = new Timer(0, 0);
        public delegate void EnergyChangeDelegate();
        public event EnergyChangeDelegate OnEnergyChanged;

        public bool CombatEnabled => EnergyLeft > 0;

        public PlayerStats PlayerStats { get; private set; }
        public BasePlayerStats BasePlayerStats;
        public List<Item> Inventory { get; private set; } = new List<Item>();

        public int MatterHarvested { get; private set; } = 0;
        public UnityEvent<float> OnMatterChanged;
        public UnityEvent<float> OnMatterIncreased;
        public UnityEvent<float> OnMatterDecreased;

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
                _energyDecayTiemr = new Timer(BasePlayerStats.EnergyDecayCooldown, Time.time);
                ChangeEnergy(-PlayerStats.EnergyDecay.Value * BasePlayerStats.EnergyDecayCooldown);
            }
        }
        public override void Initialize(int additiveHealth = 0)
        {
            base.Initialize(additiveHealth);
            PlayerStats = new PlayerStats.Builder()
                .WithEnergy(BasePlayerStats.Energy)
                .WithEnergyDecay(BasePlayerStats.EnergyDecay)
                .WithEnergyRegen(BasePlayerStats.EnergyRegeneration)
                .WithAttackDamage(BasePlayerStats.AttackDamage)
                .WithAttackCD(BasePlayerStats.AttackCooldown)
                .WithProjectileSpeed(BasePlayerStats.ProjectileSpeed)
                .WithMovementSpeed(BasePlayerStats.MovementSpeed)
                .WithRange(BasePlayerStats.AttackRange)
                .WithPenetration(BasePlayerStats.Penetration)
                .Build();
            EnergyLeft = MaxEnergy;
        }
        public void AddItem(Item itemPrefab)
        {
            var spawned = Instantiate(itemPrefab, transform);
            spawned.OnPickup(this);
            Inventory.Add(spawned);
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
            if (EnergyLeft > MaxEnergy)
                EnergyLeft = MaxEnergy;
            if (EnergyLeft <= 0)
            {
                EnergyLeft = 0;
                TakeDamage(Mathf.RoundToInt(BasePlayerStats.NoEnergyHealthDamage));
            }
            OnEnergyChanged?.Invoke();
        }
    }
}
