namespace GrampsView.Data
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface definitions for IExternal Storage.
    /// </summary>
    public interface IStoreFile
    {
        /// <summary>
        /// Initialises the data storage asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        Task<bool> DataStorageInitialiseAsync();

        /// <summary>
        /// Decompresses the gzip.
        /// </summary>
        /// <param name="grampsDataFile">
        /// The gramps data file.
        /// </param>
        /// <returns>
        /// </returns>
        Task<bool> DecompressGZIP(IFileInfoEx grampsDataFile);

        Task<bool> DecompressTAR();
    }
}