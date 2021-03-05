using GrampsView.Data.Model;

using System.IO;
using System.Threading.Tasks;

namespace GrampsView.Common.CustomClasses
{
    public interface IPlatformSpecific
    {
        Task ActivityTimeLineAdd(PersonModel argPersonModel);

        Task ActivityTimeLineAdd(FamilyModel argFamilyModel);

        // TODO Implement this
        Task<MediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, MediaModel argNewMediaModel);

        // TODO Implement this
        Task<HLinkMediaModel> GenerateThumbImageFromVideo(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, long millisecond);
    }
}