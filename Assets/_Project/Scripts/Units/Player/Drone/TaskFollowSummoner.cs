using Selivura.BehaviorTrees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class TaskFollowSummoner : Node
    {
        private Unit _summoner;
        private IMovement _movement;
        private float _stopDistance = 2;
        private Transform _transform;
        private float _speed = 5;
        public TaskFollowSummoner(Unit summoner, IMovement movement, float stopDistance, float speed,Transform transform) 
        {
            _summoner = summoner;
            _movement = movement;
            _stopDistance = stopDistance;
            _speed = speed;
            _transform = transform;
        }
        public override NodeState Evaluate()
        {
            if (_summoner == null)
            {
                state = NodeState.Failure;
                return state;
            }
            float distance = Vector2.Distance(_transform.position, _summoner.transform.position);
            if (distance <= _stopDistance)
            {
                _movement.Stop();
                state = NodeState.Succes;
                return state;
            }
            _movement.Move(_transform.position.NormalizedDirectionTo(_summoner.transform.position), _speed);
            
            state = NodeState.Running;
            return state;

        }
    }
}
