namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;

    using System.Threading.Tasks;

    /// <summary>
    /// Interface definitions for IExternal Storage.
    /// </summary>
    public interface IStorePostLoad
    {
        ItemGlyph GetThumbImageFromZip(MediaModel argMediaModel);

        Task LoadSerialUiItems();
    }
}