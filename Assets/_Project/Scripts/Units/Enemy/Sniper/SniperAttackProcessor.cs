using UnityEngine;

namespace Selivura
{
    [CreateAssetMenu]
    public class SniperAttackProcessor : ScriptableObject
    {
        public string PrepareParameterName = "AttackPrepare";
        public int AttackDamage = 25;
        public float AttackCooldown = 1;
        public float AttackPrepare = 1;
        public GameObject TargetMarkPrefab;
        public LineRenderer TargetLinePrefab;
        public AudioClip[] ShootSounds;
        public AudioClip TargetSound;
        public SoundParameters SoundParameters = new SoundParameters.Builder()
                .WithChannel(SoundChannel.SFX)
                .WithVolume(0.5f)
                .WithMinPitch(0.95f)
                .WithMaxPitch(1.05f).Build();
        public Effect HitEffect;

        public void PlayPrepareEffects(Animator animator, AudioPlayer player)
        {
            animator.SetBool(PrepareParameterName, true);
            player.PlaySound(TargetSound, SoundParameters);
        }
        public void PlayShootEffects(Animator animator, AudioPlayer player, EffectPool effectPool, Vector2 targetPosition)
        {
            player.PlaySound(ShootSounds.GetRandomElement(), SoundParameters);
            animator.SetBool(PrepareParameterName, false);
            effectPool.GetOrCreatedEffect(HitEffect).transform.position = targetPosition;
        }

    }
}
