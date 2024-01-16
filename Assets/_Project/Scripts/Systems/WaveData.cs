using System;
using UnityEngine;

namespace Selivura
{
    [CreateAssetMenu(menuName = "Enemy wave")]
    public class WaveData : ScriptableObject
    {
        public EnemyEntry[] WaveEnemies;
    }

    [Serializable]
    public class EnemyEntry
    {
        public BaseEnemyUnit EnemyPrefab;
        public int Amount = 1;
    }
}
