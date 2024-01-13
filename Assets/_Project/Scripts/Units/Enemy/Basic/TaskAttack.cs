using Selivura.BehaviorTrees;
using UnityEditor;
using UnityEngine;

namespace Selivura
{
    public class TaskAttack : Node
    {
        private int _attackDamage = 25;
        private float _attackCooldown = 1;
        Node _dataNode;
        private Timer _attackTimer = new Timer(0, 0);

        public TaskAttack(int attackDamage, float attackCooldown, Node dataNode)
        {
            _attackDamage = attackDamage;
            _attackCooldown = attackCooldown;
            _dataNode = dataNode;
            _attackTimer = new Timer(0, 0);
        }

        public override NodeState Evaluate()
        {
            Unit target = (Unit)_dataNode.GetData(FollowerEnemyBT.DataTargetKey);
            if (target == null)
            {
                state = NodeState.Failure;
                return state;
            }
            if(!_attackTimer.Expired)
            {
               state = NodeState.Running;
               return state;
            }

            _attackTimer = new Timer(_attackCooldown, Time.time);

            target.TakeDamage(_attackDamage);
            if(target.CurrentHealth <= 0)
            {
                _dataNode.ClearData(FollowerEnemyBT.DataTargetKey);
            }

            state = NodeState.Succes;
            return state;
        }
    }
}