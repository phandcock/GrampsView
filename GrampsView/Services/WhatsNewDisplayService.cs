namespace GrampsView.Services
{
    using GrampsView.Common;
    using GrampsView.Views;

    using System.Threading.Tasks;

    using Xamarin.Essentials;

    public class WhatsNewDisplayService : IWhatsNewDisplayService
    {
        private bool displayWhatsNew = true;

        public WhatsNewDisplayService()
        {
        }

        public async Task<bool> ShowIfAppropriate()
        {
            // VersionTracking.IsFirstLaunchForCurrentBuild returns true every time called when
            // first run. If this service is called multiple times then it will need the flag.
            if (VersionTracking.IsFirstLaunchForCurrentBuild && displayWhatsNew)
            {
                displayWhatsNew = false;

                await CommonRoutines.NavigateAsync(nameof(WhatsNewPage));

                return true;
            }

            return false;
        }
    }
}