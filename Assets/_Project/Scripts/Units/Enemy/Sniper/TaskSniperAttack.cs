using Selivura.BehaviorTrees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class TaskSniperAttack : Node
    {
        Node _dataNode;
        private Timer _attackCDTimer = new Timer(0, 0);
        private Timer _attackPrepareTimer = new Timer(0, 0);
        private Animator _animator;
        private GameObject _spawnedTargetMark;
        private Unit _unit;
        private bool _isPreparing;
        [Inject]
        AudioPlayer _audioPlayer;
        [Inject]
        EffectPool _effectPool;

        private SniperAttackProcessor _processor;

        public TaskSniperAttack(Unit me, Node dataNode, Animator animator, SniperAttackProcessor processor)
        {
            _dataNode = dataNode;
            _attackCDTimer = new Timer(0, 0);
            _animator = animator;
            _unit = me;
            _processor = processor;
            _unit.OnKilled.AddListener(OnKilled);
            Injector.Instance.Inject(this);
        }
        private void OnKilled(Unit a = null)
        {
            RemoveTargetMark();
            _unit.OnKilled.RemoveListener(OnKilled);
        }
        private void RemoveTargetMark()
        {
            if (_spawnedTargetMark != null)
            {
                Object.Destroy(_spawnedTargetMark);
                _spawnedTargetMark = null;
            }
        }

        public override NodeState Evaluate()
        {
            Unit target = (Unit)_dataNode.GetData(FollowerEnemyBT.DataTargetKey);
            if (target == null)
            {
                state = NodeState.Failure;
                _isPreparing = false;
                RemoveTargetMark();
                return state;
            }
            if (!_attackCDTimer.Expired)
            {
                state = NodeState.Running;
                return state;
            }
            if (!_isPreparing)
            {
                StartPreparing();
            }
            else
            {
                ApplyMarkOnTarget(target);

                if (_attackPrepareTimer.Expired)
                {
                    Shoot(target);
                    state = NodeState.Succes;
                    return state;
                }
            }
            state = NodeState.Running;
            return state;

        }

        private void StartPreparing()
        {
            Debug.Log("Preparing");
            _isPreparing = true;
            _attackPrepareTimer = new Timer(_processor.AttackPrepare, Time.time);
            _processor.PlayPrepareEffects(_animator);
        }

        private void ApplyMarkOnTarget(Unit target)
        {
            if (_spawnedTargetMark == null)
            {
                _spawnedTargetMark = Object.Instantiate(_processor.TargetMarkPrefab, target.transform.position, Quaternion.identity);
            }
            _spawnedTargetMark.transform.position = target.transform.position;
        }
        private void Shoot(Unit target)
        {
            Debug.Log("Shooting");
            _isPreparing = false;

            _processor.PlayShootEffects(_animator, _audioPlayer, _effectPool, target.transform.position);

            RemoveTargetMark();


            _attackCDTimer = new Timer(_processor.AttackCooldown, Time.time);


            target.TakeDamage(_processor.AttackDamage);
            if (target.CurrentHealth <= 0)
            {
                _dataNode.ClearData(FollowerEnemyBT.DataTargetKey);
            }
            Debug.Log("Reloading");
        }

        public class Builder
        {
            Unit _unit;
            Node _dataNode;
            Animator _animator;

            private SniperAttackProcessor _processor;
            public Builder WithUnit(Unit unit)
            {
                _unit = unit;
                return this;
            }
            public Builder WithData(Node data)
            {
                _dataNode = data;
                return this;
            }
            public Builder WithAnimator(Animator animator)
            {
                _animator = animator;
                return this;
            }
            public Builder WithProcessor(SniperAttackProcessor processor)
            {
                _processor = processor; 
                return this;
            }
            public TaskSniperAttack Build()
            {
                return new TaskSniperAttack(_unit, _dataNode, _animator, _processor);
            }
        }

    }
}
