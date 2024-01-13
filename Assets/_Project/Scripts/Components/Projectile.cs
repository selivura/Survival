using Selivura.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    [RequireComponent(typeof(IMovement))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] ProjectileData _data;
        private float _initializationTime;
        private bool _initialized;
        public UnityEvent OnHit;
        IMovement _movement;
        private void Awake()
        {
            _movement = GetComponent<IMovement>();
        }
        public void Initialize(ProjectileData data, Vector2 direction)
        {
            _data = data;
            transform.right = direction;
            _initializationTime = Time.time;
            _initialized = true;
        }
        private void FixedUpdate()
        {
            _movement.Move(transform.right, _data.Speed);
            if (Time.time - _initializationTime > _data.Lifetime && _initialized)
            {
                Deinitialize();
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Unit unit))
            {
                Hit(unit);
            }
        }
        private void Hit(Unit unit)
        {
            unit.ChangeHealth(-_data.Damage);
            OnHit?.Invoke();
            Deinitialize();
        }

        private void Deinitialize()
        {
            gameObject.SetActive(false);
            _initialized = false;
        }
    }

    [System.Serializable]
    public class ProjectileData
    {
        public int Damage;
        public float Speed;
        public float Lifetime = 4;
        public ProjectileData(int damage, float speed, float lifetime)
        {
            Damage = damage;
            Speed = speed;
            Lifetime = lifetime;
        }

        public class Builder
        {
            int _damage = 1;
            float _speed = 15;
            float _lifeime = 3;
            public Builder WithDamage(int damage)
            {
                _damage = damage;
                return this;
            }
            public Builder WithSpeed(float speed)
            {
                _speed = speed;
                return this;
            }
            public Builder WithLifetime(float lifetime)
            {
                _lifeime = lifetime;
                return this;
            }
            public ProjectileData Build()
            {
                return new ProjectileData(_damage, _speed, _lifeime);
            }
        }
    }
}
