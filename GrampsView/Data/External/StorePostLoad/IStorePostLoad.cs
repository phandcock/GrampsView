namespace GrampsView.Data.ExternalStorageNS
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface definitions for IExternal Storage.
    /// </summary>
    public interface IStorePostLoad
    {
        /// <summary>
        /// Loads the serial UI items.
        /// </summary>
        /// <returns>
        /// </returns>
        Task LoadSerialUiItems();
    }
}