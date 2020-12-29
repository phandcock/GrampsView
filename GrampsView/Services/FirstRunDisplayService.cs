namespace GrampsView.Services
{
    using GrampsView.Views;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class FirstRunDisplayService : IFirstRunDisplayService
    {
        public FirstRunDisplayService()
        {
        }

        /// <summary>
        /// Shows FirstRun pageif appropriate asynchronous.
        /// </summary>
        /// <param name="iocEventAggregator">
        /// </param>
        /// <returns>
        /// </returns>
        public bool ShowIfAppropriate()
        {
            if (VersionTracking.IsFirstLaunchEver) //  && !Common.CommonLocalSettings.FirstRunDisplay)
            {
                Common.CommonLocalSettings.FirstRunDisplay = true;
                Shell.Current.GoToAsync(nameof(FirstRunPage));

                return true;
            }

            return false;
        }
    }
}