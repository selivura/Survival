using Selivura.BehaviorTrees;
using UnityEngine;

namespace Selivura
{
    public class TaskAttack : Node
    {
        Node _dataNode;
        private Timer _attackTimer = new Timer(0, 0);
        AttackTaskData _attackData;
        [Inject]
        private AudioPlayer _audioPlayer;
        public TaskAttack(AttackTaskData attackData,Node dataNode)
        {
            GameObject.FindFirstObjectByType<Injector>().Inject(this);
            _attackData = attackData;
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
            if (!_attackTimer.Expired)
            {
               state = NodeState.Running;
               return state;
            }

            _attackTimer = new Timer(_attackData.AttackCooldown, Time.time);
            if(_audioPlayer)
                _attackData.PlayAttackeffects(_audioPlayer);
            target.TakeDamage(_attackData.AttackDamage);
            if(target.CurrentHealth <= 0)
            {
                _dataNode.ClearData(FollowerEnemyBT.DataTargetKey);
            }

            state = NodeState.Succes;
            return state;
        }
    }
}