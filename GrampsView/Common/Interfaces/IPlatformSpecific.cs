using GrampsView.Data.Model;

using System.IO;
using System.Threading.Tasks;

namespace GrampsView.Common.CustomClasses
{
    public interface IPlatformSpecific
    {
        Task ActivityTimeLineAdd(PersonModel argPersonModel);

        Task ActivityTimeLineAdd(FamilyModel argFamilyModel);

        Task<MediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, MediaModel argNewMediaModel);

        Task<MediaModel> GenerateThumbImageFromVideo(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, MediaModel argNewMediaModel);
    }
}