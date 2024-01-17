using UnityEngine;

namespace Selivura
{
    public class AutoStartingEffect : Effect
    {
        [SerializeField] protected float lifetime = 1;
        private void OnEnable()
        {
            Setup(lifetime);
        }
    }
}
