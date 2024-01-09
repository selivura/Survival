using Selivura.ObjectPooling;

namespace Selivura
{
    public class EffectPool : Singleton<EffectPool>
    {
        private PoolingSystem<Effect> _pool;
        protected override void Awake()
        {
            base.Awake();
            _pool = new PoolingSystem<Effect>(transform);
        }
        public Effect GetOrCreatedEffect(Effect prefab)
        {
            return _pool.Get(prefab);
        }
    }
}
