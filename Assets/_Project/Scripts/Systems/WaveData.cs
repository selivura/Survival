using UnityEngine;

namespace Selivura
{
    [CreateAssetMenu(menuName = "Enemy wave")]
    public class WaveData : ScriptableObject
    {
        public BaseEnemyUnit[] WaveEnemies;
    }
}
