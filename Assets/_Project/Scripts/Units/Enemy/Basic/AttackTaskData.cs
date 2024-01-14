using UnityEngine;

namespace Selivura
{
    [CreateAssetMenu]
    public class AttackTaskData : ScriptableObject
    {
        public int AttackDamage;
        public float AttackCooldown;
        public AudioClip AttackClip = null;
        public SoundParameters AttackSoundParameters;
        public float AttackRange = 2;

        public void PlayAttackeffects(AudioPlayer player)
        {
            player.PlaySound(AttackClip, AttackSoundParameters);
        }
    }
}
