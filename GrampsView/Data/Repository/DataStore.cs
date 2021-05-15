using GrampsView.Common;

namespace GrampsView.Data.Repository
{
    /// <summary>
    /// Static Data Store.
    /// </summary>

    public static class DataStore

    {
        /// <summary>
        /// Gets or sets the Application Wide Data Store.
        /// </summary>
        /// <value>
        /// The ad.
        /// </value>
        public static ApplicationWideData AD { get; set; } = new ApplicationWideData();

        /// <summary>
        /// Gets or sets the cn.
        /// </summary>
        /// <value>
        /// The cn.
        /// </value>
        public static ICommonNotifications CN
        {
            get; set;
        }

        /// <summary>
        /// Gets the Data Store.
        /// </summary>
        /// <value>
        /// The DataStore.
        /// </value>
        public static DataInstance DS { get; } = new DataInstance();
    }
}