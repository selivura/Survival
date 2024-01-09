using UnityEngine;

namespace Selivura
{
    [CreateAssetMenu(menuName = "Friendly base data")]
    public class MainBaseData : ScriptableObject
    {
        public int BaseHealth = 100;
        public int HealthPerLevel = 10;
        public float CombatRadiusPerLevel = .25f;
        public float BaseCombatRadius = 2.5f;
        public int MatterProgression = 5;
    }
}
