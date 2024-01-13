using Selivura.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class InfiniteEnergyArea : Area
    {
        [Inject]
        protected PlayerUnit _player;
        protected bool playerInRange = false;
        private void Awake()
        {
            Injector.Instance.Inject(this);
        }
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == _player.gameObject)
            {
                playerInRange = true;
            }
        }
        protected override void FixedUpdate()
        {
            if(playerInRange)
            {
                _player.InfiniteEnergy = true;
            }
            else
            {
                _player.InfiniteEnergy = false;
            }
        }
        protected override void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == _player.gameObject)
            {
                playerInRange = false;
            }
        }
    }
}
