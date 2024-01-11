using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Selivura
{
    public class AutoStartingEffect : Effect
    {
        [SerializeField] protected float lifetime = 1;
        private void Awake()
        {
            Setup(lifetime);
        }
    }
}
