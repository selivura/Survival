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
        protected override void OnTriggerStay2D(Collider2D collision)
        {
            _player.InfiniteEnergy = true;
        }
        protected override void OnTriggerExit2D(Collider2D collision)
        {
            _player.InfiniteEnergy = false;
        }
    }
}
