using Selivura.Player;
using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(Collider2D))]
    public class CombatArea : MonoBehaviour
    {
        public CircleCollider2D CircleCollider;
        private PlayerUnit _player;
        [SerializeField] private float _rechargeCooldown = .1f;
        private float _lastRechargeTime = 0;
        public bool CanCharge => Time.time - _lastRechargeTime > _rechargeCooldown && _playerInRange;
        public float CombatEnableRadius
        {
            get
            {
                return CircleCollider.radius;
            }
            set
            {
                CircleCollider.radius = value;
                OnAreaChanged?.Invoke();
            }
        }
        bool _playerInRange = false;
        public delegate void OnAreaChangeDelegate();
        public event OnAreaChangeDelegate OnAreaChanged;

        private void OnTriggerStay2D(Collider2D collision)
        {
            CheckPlayerReference();
            if (collision.gameObject == _player.gameObject)
            {
                _playerInRange = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!_player) return;
            if (collision.gameObject == _player.gameObject)
            {
                _playerInRange = false;
            }
        }
        private void FixedUpdate()
        {
            ChargePlayerEnergy();
        }
        private void CheckPlayerReference()
        {
            if (!_player)
            {
                _player = FindAnyObjectByType<PlayerUnit>();
            }
        }
        private void ChargePlayerEnergy()
        {
            if (CanCharge)
            {
                _player.ChangeEnergy(_player.EnergyRegeneration.Value * _rechargeCooldown);
                _lastRechargeTime = Time.time;
            }
        }
    }
}
