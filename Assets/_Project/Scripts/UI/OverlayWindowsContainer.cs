namespace Selivura.UI
{
    public class OverlayWindowsContainer : Singleton<OverlayWindowsContainer>, IDependecyProvider
    {
        [Provide]
        public OverlayWindowsContainer Provide()
        {
            return this;
        }
    }
}
