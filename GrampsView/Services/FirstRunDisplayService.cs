namespace GrampsView.Services
{
    using GrampsView.Common;
    using GrampsView.Views;

    using Xamarin.Essentials;

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
                CommonRoutines.Navigate(nameof(FirstRunPage));

                return true;
            }

            return false;
        }
    }
}