using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public abstract class Combat : MonoBehaviour
    {
        public UnityEvent OnAttack;
        public UnityEvent<HitInfo> OnHit;
        public abstract void Attack(Vector2 direction, AttackData data);
    }
    [System.Serializable]
    public class HitInfo
    {
        public Vector2 position;
        public Unit unit;
        public Projectile projectile;
    }

}
