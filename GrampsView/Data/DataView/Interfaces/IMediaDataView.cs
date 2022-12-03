using GrampsView.Data.Collections;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels;
using GrampsView.Models.HLinks.Interfaces;

namespace GrampsView.Data.DataView
{
    /// <summary>
    /// Interfaces for Media Repository.
    /// </summary>
    public interface IMediaDataView : IDataViewBase<MediaModel, HLinkMediaModel, HLinkMediaModelCollection>
    {
        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkMediaModelCollection GetAllAsHLink();

        HLinkMediaModelCollection GetAllNotClippedAsHLink();

        /// <summary>Gets the random from collection.</summary>
        /// <param name="theCollection">The collection.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        IHLinkMediaModel GetRandomFromCollection(HLinkMediaModelCollection theCollection);
    }
}