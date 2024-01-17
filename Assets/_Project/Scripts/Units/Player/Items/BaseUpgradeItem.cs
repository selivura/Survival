using Selivura.Player;

namespace Selivura
{
    public class BaseUpgradeItem : Item
    {
        [Inject]
        protected MainBase mainBase;
        public override void OnPickup(PlayerUnit player)
        {
            base.OnPickup(player);
            FindFirstObjectByType<Injector>().Inject(this);
            mainBase.OnLevelUp.AddListener(OnBaseLevelUp);
        }
        public override void OnRemove(PlayerUnit player)
        {
            mainBase.OnLevelUp.RemoveListener(OnBaseLevelUp);
            base.OnRemove(player);
        }
        protected virtual void OnBaseLevelUp()
        {

        }
    }
}
