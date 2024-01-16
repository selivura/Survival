using Selivura.Player;
using UnityEngine;

namespace Selivura
{
    public class Teleport : MonoBehaviour, IInteractable
    {
        public Vector2 TeleportPosition;

        public void Interact(PlayerUnit interactor)
        {
            interactor.transform.position = TeleportPosition;
        }
        public bool CanInteract(PlayerUnit interactor)
        {
            return true;
        }

        public string GetInteractionName()
        {
            return "Teleport";
        }
    }
}
