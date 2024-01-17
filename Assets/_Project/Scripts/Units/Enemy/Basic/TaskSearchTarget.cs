using Selivura.BehaviorTrees;
using UnityEngine;

namespace Selivura
{
    public class TaskSearchTarget : Node
    {
        private Transform _transform;

        private Timer _searchTimer = new Timer(0, 0);
        private float _searchCooldown = 0.5f;
        private float _searchRadius = 1;
        private LayerMask _targetLayerMask;
        private Node _dataNode;

        public TaskSearchTarget(Transform transform, float searchCooldown, float searchRadius, LayerMask targetLayerMask, Node dataNode)
        {
            _transform = transform;
            _searchCooldown = searchCooldown;
            _searchRadius = searchRadius;
            _searchTimer = new Timer(_searchCooldown, Time.time);
            _targetLayerMask = targetLayerMask;
            _dataNode = dataNode;
        }
        public override NodeState Evaluate()
        {
            if (_dataNode.GetData(FollowerEnemyBT.DataTargetKey) != null)
            {
                var newTarget = TryGetTarget();
                if (newTarget != null)
                {
                    _dataNode.SetData(FollowerEnemyBT.DataTargetKey, newTarget);
                }
            }
            else
            {
                var newTarget = TryGetTarget();
                if (newTarget == null)
                {
                    state = NodeState.Failure;
                    return state;
                }
                else
                {
                    _dataNode.SetData(FollowerEnemyBT.DataTargetKey, newTarget);
                }
            }
            state = NodeState.Succes;
            return state;
        }
        private Unit TryGetTarget()
        {
            if (!_searchTimer.Expired)
            {
                return null;
            }
            _searchTimer = new Timer(_searchCooldown, Time.time);
            var foundTargets = Physics2D.OverlapCircleAll(_transform.position, _searchRadius, _targetLayerMask);
            if (foundTargets.Length <= 0)
            {
                return null;
            }

            float nearestDistance = float.PositiveInfinity;
            Collider2D nearestTarget = foundTargets[0];

            foreach (var foundTarget in foundTargets)
            {
                float distance = Vector2.Distance(foundTarget.transform.position, _transform.position);
                if (distance < nearestDistance)
                {
                    nearestTarget = foundTarget;
                    nearestDistance = distance;
                }
            }
            if (nearestTarget.TryGetComponent(out Unit target))
            {
                return target;
            }
            return null;
        }
    }
}
