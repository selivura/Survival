using Selivura.BehaviorTrees;
using UnityEngine;

namespace Selivura
{
    public class TaskLookAtTarget : Node
    {
        private Transform _transform;
        private Node _dataNode;

        public TaskLookAtTarget(Transform transform, Node dataNode)
        {
            _transform = transform;
            _dataNode = dataNode;
        }
        public override NodeState Evaluate()
        {
            var target = (Unit)_dataNode.GetData(FollowerEnemyBT.DataTargetKey);
            if (target == null)
            {
                state = NodeState.Failure;
                return state;
            }
            _transform.right = _transform.position.NormalizedDirectionTo(target.transform.position);
            state = NodeState.Succes;
            return state;
        }
    }
}
