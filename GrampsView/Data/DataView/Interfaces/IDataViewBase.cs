namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Models.DataModels;
    using GrampsView.Models.HLinks;

    using SharedSharp.Model;

    using System.Collections.Generic;

    /// <summary>
    /// Interfaces for Base Repository.
    /// </summary>
    /// <typeparam name="T">
    /// Data ViewModel.
    /// </typeparam>
    /// <typeparam name="TU">
    /// $$(HLink)$$ ViewModel.
    /// </typeparam>
    public interface IDataViewBase<T, TU, TH>
        where TH : HLinkBaseCollection<TU>, new()
        where T : ModelBase, new()
        where TU : HLinkBase, new()
    {
        int Count
        {
            get;
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        IReadOnlyList<T> DataDefaultSort
        {
            get;
        }

        IReadOnlyList<T> DataViewData
        {
            get;
        }

        TH GetLatestChanges
        {
            get;
        }

        CardGroupHLink<TU> AsCardGroup(IReadOnlyList<TU> argReadOnlyList);

        TH GetAllAsCardGroupBase();

        Group<TH> GetAllAsGroupedCardGroup();

        /// <summary>
        /// Gets all as ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        List<T> GetAllAsModel();

        ItemGlyph GetGlyph(HLinkKey argHLinkKey);

        /// <summary>
        /// Gets the model from the hlink. Helper method.
        /// </summary>
        /// <param name="HLink">
        /// The h link.
        /// </param>
        /// <returns>
        /// Model from HLink.
        /// </returns>
        T GetModelFromHLink(HLinkBase HLink);

        /// <summary>
        /// Gets the specified h link string.
        /// </summary>
        /// <param name="HLinkString">
        /// The h link string.
        /// </param>
        /// <returns>
        /// </returns>
        T GetModelFromHLinkKey(HLinkKey argHLinkKey);

        T GetModelFromId(string argId);

        /// <summary>
        /// Gets the model information formatted.
        /// </summary>
        /// <param name="argModel">
        /// The argument ViewModel.
        /// </param>
        /// <returns>
        /// </returns>
        CardListLineCollection GetModelInfoFormatted(ModelBase argModel);

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        TH HLinkCollectionSort(TH collectionArg);

        /// <summary>
        /// Searches the specified query string.
        /// </summary>
        /// <param name="queryString">
        /// The query string.
        /// </param>
        /// <returns>
        /// CardGroupBase list
        /// </returns>
        TH Search(string queryString);
    }
}