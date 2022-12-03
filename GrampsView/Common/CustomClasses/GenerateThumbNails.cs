using GrampsView.Common.Interfaces;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Interfaces;

namespace GrampsView.Common.CustomClasses
{
    public partial class GenerateThumbNails : IGenerateThumbnails
    {
        public GenerateThumbNails()
        {

        }

        public Task<IMediaModel> GenerateThumbImageFromPDF(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel)
        {
            throw new NotImplementedException();
        }

        public Task<IMediaModel> GenerateThumbImageFromVideo(DirectoryInfo argCurrentDataFolder, MediaModel argExistingMediaModel, IMediaModel argNewMediaModel)
        {
            throw new NotImplementedException();
        }
    }
}
