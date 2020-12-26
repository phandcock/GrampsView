namespace GrampsView.Data.Repository
{
    using GrampsView.Common;

    public interface IDataStore
    {
        ApplicationWideData AD { get; set; }

        /// <summary>
        /// Gets or sets the cn.
        /// </summary>
        /// <value>
        /// The cn.
        /// </value>
        ICommonNotifications CN { get; set; }

        /// <summary>
        /// Gets the Data Store.
        /// </summary>
        /// <value>
        /// The datastore.
        /// </value>
        DataInstance DS { get; }

        //NavCmd NV { get; set; }
    }
}