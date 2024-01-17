using Selivura.BehaviorTrees;
using UnityEngine;

namespace Selivura
{
    public class CheckTargetInAttackRange : Node
    {
        private float _attackRange = 1;
        private Transform _transform;
        private Node _dataNode;
        private IMovement _movement;

        public CheckTargetInAttackRange(Transform transform, float attackRange, Node dataNode, IMovement movement = null)
        {
            _transform = transform;
            _attackRange = attackRange;
            _dataNode = dataNode;
            _movement = movement;
        }
        public override NodeState Evaluate()
        {
            Unit target = (Unit)_dataNode.GetData(FollowerEnemyBT.DataTargetKey);
            if (target == null)
            {
                state = NodeState.Failure;
                return state;
            }
            float targetDistance = Vector2.Distance(target.transform.position, _transform.position);
            if (targetDistance <= _attackRange)
            {
                if (_movement != null)
                {
                    _movement.Stop();
                }
                state = NodeState.Succes;
                return state;
            }
            state = NodeState.Failure;
            return state;
        }
    }
}