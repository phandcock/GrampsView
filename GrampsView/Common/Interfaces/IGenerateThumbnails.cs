namespace GrampsView.Common.Interfaces
{
    using GrampsView.Models.DataModels;
    using GrampsView.Models.DataModels.Interfaces;

    using System.IO;
    using System.Threading.Tasks;

    public interface IGenerateThumbnails
    {
        Task<IMediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel);

        Task<IMediaModel> GenerateThumbImageFromVideo(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel);
    }
}