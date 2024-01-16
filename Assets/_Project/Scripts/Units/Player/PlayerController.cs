using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Selivura.Player
{
    [RequireComponent(typeof(PlayerUnit))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerInteractChecker))]
    public class PlayerController : MonoBehaviour
    {
        public const float NoEnergySpeedPenaltyMultiplier = .5f;
        PlayerInput _playerInput;
        private IMovement _movement;
        private Combat _combat;
        public Vector2 MovementInput { get; private set; }
        public bool AttackInput { get; private set; }
        public bool InteractInput { get; private set; }
        public Vector2 DirectionInput => Utilities.GetMouseDirection(_cam, transform.position);

        private PlayerUnit _playerUnit;
        Camera _cam;
        private PlayerInteractChecker _interactChecker;

        private PlayerControlActions _inputActions;
        private string _moveActionName => _inputActions.Player.Move.name;
        private string _attackActionName => _inputActions.Player.Fire.name;
        private string _interactActionName => _inputActions.Player.Interact.name;
        //private string _directionActionName => _inputActions.Player.Interact.name;

        [Inject]
        PauseController _pauseController;
        private void Awake()
        {
             FindFirstObjectByType<Injector>().Inject(this);
        }
        private void OnEnable()
        {
            _cam = Camera.main;
            _inputActions = new PlayerControlActions();
            _playerInput = GetComponent<PlayerInput>();
            _movement = GetComponent<IMovement>();
            _combat = GetComponent<Combat>();
            _playerUnit = GetComponent<PlayerUnit>();
            _interactChecker = GetComponent<PlayerInteractChecker>();
        }
        private void FixedUpdate()
        {
            if (_movement != null)
            {
                if (_playerUnit.EnergyLeft > 0)
                {
                    _movement.Move(MovementInput, _playerUnit.PlayerStats.MovementSpeed.Value);
                }
                else
                {
                    _movement.Move(MovementInput, _playerUnit.PlayerStats.MovementSpeed.Value * NoEnergySpeedPenaltyMultiplier);
                }

            }
        }
        private void Update()
        {
            if (_pauseController.IsGamePaused)
                return;
            MovementInput = _playerInput.actions.FindAction(_moveActionName).ReadValue<Vector2>();
            AttackInput = _playerInput.actions.FindAction(_attackActionName).IsPressed();
            InteractInput = _playerInput.actions.FindAction(_interactActionName).WasPressedThisFrame();

            if (_combat != null)
                if (AttackInput && _playerUnit.CombatEnabled)
                {
                    Vector2 dir = Utilities.GetMouseDirection(_cam, transform.position);
                    int damage = Mathf.RoundToInt(_playerUnit.PlayerStats.AttackDamage.Value);
                    AttackData attackData = new AttackData(
                            damage,
                            _playerUnit.PlayerStats.AttackCooldown.Value,
                            _playerUnit.PlayerStats.ProjectileSpeed.Value,
                            _playerUnit.PlayerStats.AttackRange.Value,
                            Mathf.RoundToInt(_playerUnit.PlayerStats.Penetration.Value)
                            );
                    _combat.Attack(dir, attackData);
                }
            if (InteractInput)
            {
                _interactChecker.Interact();
            }
        }
        
    }
}
