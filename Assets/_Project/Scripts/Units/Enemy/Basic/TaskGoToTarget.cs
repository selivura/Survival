using Pathfinding;
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

        private Seeker _seeker;
        private Path _path;
        private int _currentWaypoint = 0;
        private float _nextWaypointDistance = 1;
        protected Vector3 _movementDirection;

        private Node _dataNode;
        private bool _reachedEndOfPath;

        public TaskGoToTarget(Transform transform, float moveSpeed, IMovement movement, float stopDistance, Node dataNode, Seeker seeker)
        {
            _transform = transform;
            _moveSpeed = moveSpeed;
            _movement = movement;
            _stopDistance = stopDistance;
            _dataNode = dataNode;
            _seeker = seeker;
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
            UpdatePath();
            _movement.Move(_transform.position.NormalizedDirectionTo(_movementDirection), _moveSpeed);
                
            state = NodeState.Running;
            return state;
        }

        public void UpdatePath()
        {
            if (_path == null)
            {
                return;
            }
            _reachedEndOfPath = false;
            float distanceToWaypoint;
            while (true)
            {
                distanceToWaypoint = Vector3.Distance(_transform.position, _path.vectorPath[_currentWaypoint]);
                if (distanceToWaypoint < _nextWaypointDistance)
                {
                    if (_currentWaypoint + 1 < _path.vectorPath.Count)
                    {
                        _currentWaypoint++;
                    }
                    else
                    {
                        _reachedEndOfPath = true;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (!_reachedEndOfPath)
                _movementDirection = GetWaypointDirection();
            else
                _movementDirection = Vector3.zero;
        }
        private Vector3 GetWaypointDirection()
        {
            if (_path != null)
                return _transform.position.NormalizedDirectionTo(_path.vectorPath[_currentWaypoint]);
            else
                return Vector3.zero;
        }
        public void OnPathComplete(Path path)
        {
            if (!path.error)
            {
                _path = path;
                _currentWaypoint = 0;
            }
        }
    }
}
