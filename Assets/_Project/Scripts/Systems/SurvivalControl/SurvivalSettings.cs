using UnityEngine;

namespace Selivura
{
    [CreateAssetMenu]
    public class SurvivalSettings : ScriptableObject
    {
        [Header("Spawn limits")]
        public float MinEnemySpwanRange = 15;
        public float MaxEnemySpwanRange = 25;
        public float MinSpawnRangeFromBase = 20;
        public Vector2 EnemySpwanLimitation = new Vector2(50, 50);

        [Header("Peaceful phase settings")]
        public float PeacePhaseTime = 30;

        [Header("Enemy spawn settings")]
        public float EnemyHealthPerDifficultyMultiplier = 1.5f;
        public float EnemySpawnCooldown = .5f;
        public WaveData[] WaveDatas;
    }
}
