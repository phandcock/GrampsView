namespace GrampsView.Services
{
    using GrampsView.Views;

    using System.Threading.Tasks;

    using Xamarin.Essentials;

    public class FirstRunDisplayService : IFirstRunDisplayService
    {
        public FirstRunDisplayService()
        {
        }

        /// <summary>
        /// Shows FirstRun page if appropriate.
        /// </summary>
        /// <returns>
        /// </returns>
        public async Task<bool> ShowIfAppropriate()
        {
            if (VersionTracking.IsFirstLaunchEver && !SharedSharp.Misc.LocalSettings.FirstRunDisplay)
            {
                SharedSharp.Misc.LocalSettings.FirstRunDisplay = true;

                await SharedSharp.CommonRoutines.CommonRoutines.NavigateAsync(nameof(FirstRunPage));

                return true;
            }

            return false;
        }
    }
}