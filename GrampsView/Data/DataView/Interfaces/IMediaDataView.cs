namespace GrampsView.Data.DataView
{
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;

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

        /// <summary>
        /// Gets the random from collection.
        /// </summary>
        /// <param name="theCollection">
        /// The collection.
        /// </param>
        /// <param name="DefaultHLink">
        /// The default h link.
        /// </param>
        /// <returns>
        /// </returns>
        IHLinkMediaModel GetRandomFromCollection(HLinkMediaModelCollection theCollection);
    }
}