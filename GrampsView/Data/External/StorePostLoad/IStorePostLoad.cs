namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Models.DataModels;

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