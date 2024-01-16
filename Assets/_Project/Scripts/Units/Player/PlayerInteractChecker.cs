using Selivura.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    [RequireComponent(typeof(PlayerUnit))]
    public class PlayerInteractChecker : MonoBehaviour
    {
        PlayerUnit _playerUnit;
        [SerializeField] private float _interactDistance;
        [SerializeField] private LayerMask _interactableLayers;
        private List<IInteractable> _availableInteractables = new List<IInteractable>();

        public UnityEvent<string> OnInteractibleNearby;
        public UnityEvent OnLockedNearby;
        public UnityEvent OnNoInteractiblesFound;
        private void Awake()
        {
            _playerUnit = GetComponent<PlayerUnit>();
        }
        public void Interact()
        {
            foreach (var interactable in _availableInteractables)
            {
                interactable.Interact(_playerUnit);
            }
        }
        private void FixedUpdate()
        {
            InteractCheck();
        }
        private void InteractCheck()
        {
            _availableInteractables.Clear();
            var interactables = FindInteractables(DoScanForInteractables().ToArray());

            bool atLeastOneAvailable = false;
            bool lockedNearby = false;
            string avialableInteractible = "";
            foreach (var interactable in interactables)
            {
                _availableInteractables.Add(interactable);
                if (interactable.CanInteract(_playerUnit))
                {
                    if(interactable.GetInteractionName() != "")
                        avialableInteractible = interactable.GetInteractionName();
                    atLeastOneAvailable = true;
                }
                else
                {
                    lockedNearby = true;
                }
            }
            if (atLeastOneAvailable)
            {
                OnInteractibleNearby?.Invoke(avialableInteractible);
                return;
            }
            if(lockedNearby)
            {
                OnLockedNearby?.Invoke();
                return;
            }
            OnNoInteractiblesFound?.Invoke();
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
