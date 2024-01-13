using UnityEngine;

namespace Selivura
{
    [CreateAssetMenu(menuName = "Friendly base data")]
    public class MainBaseData : ScriptableObject
    {
        public int BaseHealth = 100;
        public int HealthPerLevel = 10;
        public float EnergyRegenRadiusPerLevel = .25f;
        public float BaseEnergyRegenRadius = 2.5f;
        public int XPProgression = 5;
        public float RegenerationPercent = .1f;
    }
}
