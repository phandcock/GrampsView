using GrampsView.Common.Interfaces;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Interfaces;

namespace GrampsView.Common.CustomClasses
{
    public partial class GenerateThumbNails : IGenerateThumbnails
    {

        public partial Task<IMediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel);


        public partial Task<IMediaModel> GenerateThumbImageFromVideo(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel);

    }
}
