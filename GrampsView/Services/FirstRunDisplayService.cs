namespace GrampsView.Services
{
    using GrampsView.Common;
    using GrampsView.Views;

    using System.Threading.Tasks;

    using Xamarin.Essentials;

    public class FirstRunDisplayService : IFirstRunDisplayService
    {
        public FirstRunDisplayService()
        {
        }

        /// <summary>
        /// Shows FirstRun pageif appropriate asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        public async Task<bool> ShowIfAppropriate()
        {
            if (VersionTracking.IsFirstLaunchEver && !Common.CommonLocalSettings.FirstRunDisplay)
            {
                Common.CommonLocalSettings.FirstRunDisplay = true;

                await CommonRoutines.NavigateAsync(nameof(FirstRunPage));

                return true;
            }

            return false;
        }
    }
}