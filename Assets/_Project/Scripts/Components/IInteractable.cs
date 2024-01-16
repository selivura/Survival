using Selivura.Player;

namespace Selivura
{
    public enum InteractType
    {
        Available,
        Locked, //means can be available in the future(with condition)
        Unavailable
    }

    public interface IInteractable
    {
        public void Interact(PlayerUnit interactor);
        public bool CanInteract(PlayerUnit interactor);

        public string GetInteractionName();
    }
}
