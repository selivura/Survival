using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class MatterSource : MonoBehaviour
    {
        [Inject]
        protected MatterSpawner matterSpawner;
        [SerializeField] protected List<MatterCollectible> collectibles = new List<MatterCollectible>();
        protected virtual void Awake()
        {
            FindFirstObjectByType<Injector>().Inject(this);
        }
    }
}
