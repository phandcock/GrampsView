namespace GrampsView.Data.Repository
{
    using SharedSharp.Errors.Interfaces;

    public interface IDataStore
    {
        ApplicationWideData AD { get; set; }

        /// <summary>
        /// Gets or sets the cn.
        /// </summary>
        /// <value>
        /// The cn.
        /// </value>
        IErrorNotifications CN { get; set; }

        /// <summary>
        /// Gets the Data Store.
        /// </summary>
        /// <value>
        /// The datastore.
        /// </value>
        DataInstance DS { get; }
    }
}