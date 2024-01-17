using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class Ricoshot : MonoBehaviour
    {
        [SerializeField] float _delay = 0.05f;
        [SerializeField] int _jumps = 4;
        int _jumpsLeft = 4;
        [SerializeField] int _damage = 5;
        [SerializeField] float _range = 2;
        [SerializeField] float _minRange = 0.1f;
        [SerializeField] LayerMask _layers;
        Timer _delayTimer = new Timer(0, 0);
        List<Unit> _hitTargets = new List<Unit>();
        private void OnEnable()
        {
            _jumpsLeft = _jumps;
            _hitTargets.Clear();
        }
        private void FixedUpdate()
        {
            if (!_delayTimer.Expired)
                return;
            JumpToNearest();
        }
        private void JumpToNearest()
        {
            var foundTargets = Physics2D.OverlapCircleAll(transform.position, _range, _layers);
            if (foundTargets.Length <= 0)
            {
                _jumpsLeft = 0;
            }
            if (_jumpsLeft > 1)
            {
                for (int i = 0; i < foundTargets.Length; i++)
                {
                    var target = foundTargets[i];
                    if (!target.TryGetComponent(out Unit unit))
                        continue;
                    if (_hitTargets.Contains(unit))
                        continue;
                    if (Vector2.Distance(target.transform.position, transform.position) < _minRange)
                        continue;
                    transform.position = target.transform.position;
                    unit.TakeDamage(_damage);
                    _hitTargets.Add(unit);
                    _jumpsLeft--;
                    _delayTimer = new Timer(_delay, Time.time);
                    return;
                }
            }
            gameObject.SetActive(false);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _range);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _minRange);
        }
    }
}
