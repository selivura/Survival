using Selivura.BehaviorTrees;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(IMovement))]
    public class DroneBT : BehaviourTree
    {
        [SerializeField] float _searchDistance = 25;
        [SerializeField] LayerMask _targetLayerMask = 1 << 7;

        public Unit Summoner;
        [SerializeField] float _summonerFollowStopDistance = 5;

        [SerializeField] float _targetSearchCooldown = 1;
        [SerializeField] AttackTaskData _attackData;

        [SerializeField] float _moveSpeed = 1;
        IMovement _movement;
        private Node _dataNode = new Node(new List<Node>());
        public const string DataTargetKey = "target";
        private void Awake()
        {
            _movement = GetComponent<IMovement>();
        }
        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckTargetInAttackRange(transform, _attackData.AttackRange, _dataNode, _movement),
                    new TaskAttack(_attackData, _dataNode),
                    new TaskLookAtTarget(transform, _dataNode)
                }),
                new IfNode
                (
                    new TaskSearchTarget(transform,_targetSearchCooldown, _searchDistance, _targetLayerMask, _dataNode),
                    new TaskGoToTarget(transform, _moveSpeed, _movement, _attackData.AttackRange, _dataNode),
                    new TaskFollowSummoner(Summoner, _movement, _summonerFollowStopDistance, _moveSpeed, transform)
                ),
            });

            return root;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _searchDistance);
            if (!_attackData)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackData.AttackRange);
        }
    }
}
