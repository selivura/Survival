using Selivura.BehaviorTrees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(IMovement))]
    [RequireComponent(typeof(Unit))]
    public class SniperEnemyBT : BehaviourTree
    {
        [SerializeField] float _searchDistance = 25;
        [SerializeField] LayerMask _targetLayerMask = 1 << 10 | 1 << 11;
        [SerializeField] float _targetSearchCooldown = 1;

        [SerializeField] float _attackDistance = 2;

        [SerializeField] Animator _animator;
        [SerializeField] SniperAttackProcessor _sniperAttackProcessor;

        [SerializeField] float _moveSpeed = 1;
        IMovement _movement;
        private Node _dataNode = new Node(new List<Node>());
        private void Awake()
        {
            _movement = GetComponent<IMovement>();
        }
        protected override Node SetupTree()
        {
            Node root = new ScriptNode(new List<Node>
            {
                new CheckTargetInAttackRange(transform, _attackDistance, _dataNode, _movement),
                    new TaskSniperAttack.Builder()
                        .WithUnit(GetComponent<Unit>())
                        .WithData(_dataNode)
                        .WithAnimator(_animator)
                        .WithProcessor(_sniperAttackProcessor)
                        .Build(),

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
