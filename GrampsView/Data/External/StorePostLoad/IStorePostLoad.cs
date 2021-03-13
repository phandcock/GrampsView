namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Data.Model;

    using System.Threading.Tasks;

    /// <summary>
    /// Interface definitions for IExternal Storage.
    /// </summary>
    public interface IStorePostLoad
    {
        Task<ItemGlyph> GetThumbImageFromZip(MediaModel argMediaModel);

        Task LoadSerialUiItems();
    }
}