using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class MatterSource : MonoBehaviour
    {
        [Inject]
        protected MatterSpawner matterSpawner;
        protected virtual void Awake()
        {
             FindFirstObjectByType<Injector>().Inject(this);
        }
    }
}
