using System.Collections.Generic;
using UnityEngine;

namespace Selivura.Player
{
    [RequireComponent(typeof(PlayerUnit))]
    public class PlayerController : MonoBehaviour
    {
        public const float NoEnergySpeedPenaltyMultiplier = .5f;
        PlayerControlActions _inputActions;
        public Vector2 MovementInput => _inputActions.Player.Move.ReadValue<Vector2>();
        private IMoveable _movement;
        private Combat _combat;
        public bool AttackInput => _inputActions.Player.Fire.IsPressed();
        public bool InteractInput => _inputActions.Player.Interact.WasPressedThisFrame();
        public Vector2 DirectionInput => Utilities.GetMouseDirection(_cam, transform.position);

        private PlayerUnit _playerUnit;
        [SerializeField] private float _interactDistance;
        [SerializeField] private GameObject _interactIndicator;
        [SerializeField] private GameObject _lockedinteractIndicator;
        [SerializeField] private LayerMask _interactableLayers;
        Camera _cam;
        private List<IInteractable> _availableInteractables = new List<IInteractable>();
        private void OnEnable()
        {
            _cam = Camera.main;
            _inputActions = new PlayerControlActions();
            _inputActions.Enable();
            _movement = GetComponent<IMoveable>();
            _combat = GetComponent<Combat>();
            _playerUnit = GetComponent<PlayerUnit>();
        }
        private void OnDisable()
        {
            _inputActions.Disable();
        }
        private void FixedUpdate()
        {
            if (_movement != null)
            {
                if (_playerUnit.EnergyLeft > 0)
                {
                    _movement.Move(MovementInput, _playerUnit.MovementSpeed.Value);
                }
                else
                {
                    _movement.Move(MovementInput, _playerUnit.MovementSpeed.Value * NoEnergySpeedPenaltyMultiplier);
                }

            }
            InteractCheck();
        }
        private void Update()
        {
            if (_combat != null)
                if (AttackInput && _playerUnit.CombatEnabled)
                {
                    Vector2 dir = Utilities.GetMouseDirection(_cam, transform.position);
                    int damage = Mathf.RoundToInt(_playerUnit.AttackDamage.Value);
                    AttackData attackData =
                        new AttackData(damage, _playerUnit.AttackCooldown.Value, _playerUnit.ProjectileSpeed.Value, _playerUnit.AttackRange.Value);
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
