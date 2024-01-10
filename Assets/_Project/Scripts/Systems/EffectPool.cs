using Selivura.ObjectPooling;
using UnityEngine;

namespace Selivura
{
    public class EffectPool : MonoBehaviour, IDependecyProvider
    {
        private PoolingSystem<Effect> _pool;
        [Provide]
        public EffectPool Provide()
        {
            return this;
        }
        protected void Awake()
        {
            _pool = new PoolingSystem<Effect>(transform);
        }
        public Effect GetOrCreatedEffect(Effect prefab)
        {
            return _pool.Get(prefab);
        }
    }
}
