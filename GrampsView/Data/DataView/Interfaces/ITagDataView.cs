namespace GrampsView.Data.DataView
{
    using GrampsView.Data.Collections;
    using GrampsView.Data.DataView.Interfaces;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    public interface ITagDataView : IDataViewBase<TagModel, HLinkTagModel, HLinkTagModelCollection>
    {
        RepositoryModelDictionary<TagModel, HLinkTagModel> TagData
        {
            get;
        }

        //CardGroup GetAllAsGroupedCardGroup();

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkTagModelCollection GetAllAsHLink();
    }
}