namespace GrampsView.Common.CustomClasses
{
    using GrampsView.Data.Model;
    using GrampsView.Models.DataModels;

    using System.IO;
    using System.Threading.Tasks;

    public interface IPlatformSpecific
    {
        Task<IMediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel);

        Task<IMediaModel> GenerateThumbImageFromVideo(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel);
    }
}