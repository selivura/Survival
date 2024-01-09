using Selivura.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class InfiniteEnergyArea : MonoBehaviour
    {
        public CircleCollider2D CircleCollider;
        public float Radius
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
        [Inject]
        protected PlayerUnit _player;
        protected bool playerInRange = false;

        public delegate void OnAreaChangeDelegate();
        public event OnAreaChangeDelegate OnAreaChanged;
        private void Awake()
        {
            Injector.Instance.Inject(this);
        }
        protected virtual void OnTriggerStay2D(Collider2D collision)
        {
            _player.InfiniteEnergy = true;
        }
        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            _player.InfiniteEnergy = false;
        }
    }
}
