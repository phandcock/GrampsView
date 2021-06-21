namespace GrampsView.Common.CustomClasses
{
    using GrampsView.Data.Model;

    using System.IO;
    using System.Threading.Tasks;

    public interface IPlatformSpecific
    {
        Task ActivityTimeLineAdd(PersonModel argPersonModel);

        Task ActivityTimeLineAdd(FamilyModel argFamilyModel);

        Task<IMediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel);

        Task<IMediaModel> GenerateThumbImageFromVideo(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel);
    }
}