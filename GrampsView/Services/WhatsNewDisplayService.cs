namespace GrampsView.Services
{
    using System.Threading.Tasks;

    using Xamarin.Essentials;

    public class WhatsNewDisplayService : IWhatsNewDisplayService
    {
        public WhatsNewDisplayService()
        {
        }

        public async Task<bool> ShowIfAppropriate()
        {
            // VersionTracking.IsFirstLaunchForCurrentBuild returns true every time called when
            // first run. If this service is called multiple times then it will need the flag.
            if ((VersionTracking.IsFirstLaunchForCurrentBuild)) // && (!Common.CommonLocalSettings.WhatsNewDisplayed))
            {
                return true;
            }

            return false;
        }
    }
}