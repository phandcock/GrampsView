namespace GrampsView.Services
{
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
        /// <param name="iocEventAggregator">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<bool> ShowIfAppropriate()
        {
            if (VersionTracking.IsFirstLaunchEver) //  && !Common.CommonLocalSettings.FirstRunDisplay)
            {
                Common.CommonLocalSettings.FirstRunDisplay = true;

                return true;
            }

            return false;
        }
    }
}