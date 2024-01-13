using System;
using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected int _currentHealth = 100;
        [SerializeField] private int _baseHealth = 100;
        protected int _maxHealth = 100;
        public int MaxHealth { get { return _maxHealth; } }
        public int BaseHealth { get { return _baseHealth; } }
        public int CurrentHealth { get { return _currentHealth; } }

        public UnityEvent<Unit> OnKilled;
        public UnityEvent<Unit> OnHealthChanged;
        public UnityEvent<int> OnHealthDecreased;
        public UnityEvent<int> OnHealthIncreased;
        public UnityEvent<Unit> OnDeinitialized;
        public UnityEvent<Unit> OnInitialized;
        public bool InitializeOnEnable = false;
        private void OnEnable()
        {
            if (InitializeOnEnable)
                Initialize();
        }
        public virtual void Initialize(int additiveHealth = 0)
        {
            _maxHealth = _baseHealth + additiveHealth;
            _currentHealth = _maxHealth;
            OnInitialized?.Invoke(this);
        }
        public virtual bool ChangeHealth(int value)
        {
            _currentHealth += value;
            OnHealthChanged?.Invoke(this);
            if (value < 0)
                OnHealthDecreased?.Invoke(value);
            if (value > 0)
                OnHealthIncreased?.Invoke(value);
            if (_currentHealth <= 0)
            {
                OnKilled?.Invoke(this);
                Deinitialize();
            }
            return true;
        }
        public void TakeDamage(int value)
        {
            ChangeHealth(-value);
        }
        public void Heal(int value)
        {
            ChangeHealth(value);
        }
        public virtual void Deinitialize()
        {
            OnDeinitialized?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
