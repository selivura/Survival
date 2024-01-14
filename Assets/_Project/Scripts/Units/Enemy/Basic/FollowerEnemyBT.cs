using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Selivura.BehaviorTrees;

namespace Selivura
{
    [RequireComponent(typeof(IMovement))]
    public class FollowerEnemyBT : BehaviourTree
    {
        [SerializeField] float _searchDistance = 25;
        [SerializeField] LayerMask _targetLayerMask = 1 << 10 | 1 << 11;
        [SerializeField] float _targetSearchCooldown = 1;

        [SerializeField] float _attackDistance = 2;
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
            Node root = new ScriptNode(new List<Node>
            {
                new CheckTargetInAttackRange(transform, _attackDistance, _dataNode, _movement),
                new TaskAttack(_attackData, _dataNode),
                new Sequence(new List<Node>
                {
                    new TaskSearchTarget(transform,_targetSearchCooldown, _searchDistance, _targetLayerMask, _dataNode),
                    new TaskGoToTarget(transform, _moveSpeed, _movement, _attackDistance, _dataNode),
                }),
            });
             
            return root;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _searchDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackDistance);
        }
    }
}
