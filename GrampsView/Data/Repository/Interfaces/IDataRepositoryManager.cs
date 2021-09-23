namespace GrampsView.Data.Repository
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface definitions for IGrampsRepository.
    /// </summary>
    public interface IDataRepositoryManager
    {
        /// <summary>
        /// Gets the storage.
        /// </summary>
        /// <value>
        /// The storage.
        /// </value>
        IStoreFile Storage
        {
            get;
        }

        void StartDataLoad();

        /// <summary>
        /// Triggers the load gramps un zipped folder asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        Task<bool> TriggerLoadGrampsUnZippedFolderAsync();

        /// <summary>
        /// Loads the data asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        Task<bool> TriggerLoadSerialDataAsync();
    }
}