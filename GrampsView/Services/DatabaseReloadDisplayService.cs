namespace GrampsView.Services
{
    using GrampsView.Common;

    using System.Threading.Tasks;

    public class DatabaseReloadDisplayService : IDatabaseReloadDisplayService
    {
        public DatabaseReloadDisplayService()
        {
        }

        /// <summary>
        /// Shows database reload view if appropriate.
        /// </summary>
        /// <returns>
        /// if the view is displayed.
        /// </returns>
        public async Task<bool> ShowIfAppropriate()
        {
            if (CommonLocalSettings.DatabaseReloadNeeded)
            {
                return true;
            }

            return false;
        }
    }
}