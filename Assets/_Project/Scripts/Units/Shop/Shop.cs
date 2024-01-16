using Selivura.Player;
using UnityEngine.Events;

namespace Selivura
{
    public class Shop : Unit, IInteractable
    {
        public Item ItemPrefab;
        public bool DeactivateOnPurchase = true;
        public UnityEvent OnPurchase;
        public UnityEvent OnReject;
        public void Interact(PlayerUnit interactor)
        {
            if (CanInteract(interactor))
            {
                interactor.ChangeMatter(-ItemPrefab.Price);
                interactor.AddItem(ItemPrefab);
                OnPurchase?.Invoke();
                if (DeactivateOnPurchase)
                {
                    Deinitialize();
                }
            }
            else
            {
                OnReject?.Invoke();
            }
        }
        public bool CanBuy(float money)
        {
            return money >= ItemPrefab.Price;
        }
        public bool CanInteract(PlayerUnit interactor)
        {
            return CanBuy(interactor.MatterHarvested);
        }
        public string GetInteractionName()
        {
            return "Buy";
        }
    }
}
