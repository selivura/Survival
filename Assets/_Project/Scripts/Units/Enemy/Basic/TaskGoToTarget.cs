using Selivura.BehaviorTrees;
using UnityEngine;

namespace Selivura
{
    public class TaskGoToTarget : Node
    {
        private Transform _transform;
        private IMovement _movement;

        private float _moveSpeed = 1;
        private float _stopDistance;
        private Node _dataNode;
        public TaskGoToTarget(Transform transform, float moveSpeed, IMovement movement, float stopDistance, Node dataNode)
        {
            _transform = transform;
            _moveSpeed = moveSpeed;
            _movement = movement;
            _stopDistance = stopDistance;
            _dataNode = dataNode;
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
            if (targetDistance <= _stopDistance)
            {
                _movement.Stop();
                state = NodeState.Succes;
                return state;
            }
            _movement.Move(_transform.position.NormalizedDirectionTo(target.transform.position), _moveSpeed);
                
            state = NodeState.Running;
            return state;
        }
    }
}
