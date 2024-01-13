using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Selivura.Player
{
    [RequireComponent(typeof(PlayerUnit))]
    [RequireComponent(typeof(PlayerInput))]
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
        [SerializeField] private float _interactDistance;
        [SerializeField] private GameObject _interactIndicator;
        [SerializeField] private GameObject _lockedinteractIndicator;
        [SerializeField] private LayerMask _interactableLayers;
        Camera _cam;
        private List<IInteractable> _availableInteractables = new List<IInteractable>();

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
            InteractCheck();
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
                    AttackData attackData =
                        new AttackData(damage, _playerUnit.PlayerStats.AttackCooldown.Value, _playerUnit.PlayerStats.ProjectileSpeed.Value, _playerUnit.PlayerStats.AttackRange.Value);
                    _combat.Attack(dir, attackData);
                }
            if (InteractInput)
            {
                foreach (var interactable in _availableInteractables)
                {
                    interactable.Interact(_playerUnit);
                }
            }
        }
        private void InteractCheck()
        {
            _availableInteractables.Clear();
            var interactables = FindInteractables(DoScanForInteractables().ToArray());

            _interactIndicator.SetActive(false);
            _lockedinteractIndicator.SetActive(false);

            bool atLeastOneAvailable = false;
            foreach (var interactable in interactables)
            {
                _availableInteractables.Add(interactable);
                if (interactable.CanInteract(_playerUnit))
                {
                    atLeastOneAvailable = true;
                }
                else
                {
                    _lockedinteractIndicator.SetActive(true);
                }
            }
            if (atLeastOneAvailable)
            {
                _interactIndicator.SetActive(true);
                _lockedinteractIndicator.SetActive(false);
            }
        }
        private List<GameObject> DoScanForInteractables()
        {
            var gos = new List<GameObject>();
            foreach (var item in Physics2D.OverlapCircleAll(transform.position, _interactDistance, _interactableLayers))
            {
                gos.Add(item.gameObject);
            }
            return gos;
        }
        private List<IInteractable> FindInteractables(GameObject[] objects)
        {
            List<IInteractable> interactables = new List<IInteractable>();
            foreach (var item in objects)
            {
                interactables.AddRange(item.GetComponents<IInteractable>());
            }
            return interactables;
        }
    }
}
