using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public abstract class Combat : MonoBehaviour
    {
        public UnityEvent OnAttack;
        public abstract void Attack(Vector2 direction, AttackData data);
    }
}
