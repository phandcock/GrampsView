// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Model;
using GrampsView.DBModels;
using GrampsView.Models.HLinks;

namespace GrampsView.Data.DataLayer.Interfaces
{
    /// <summary>Interfaces for Base Repository.</summary>
    /// <typeparam name="T">Data ViewModel.</typeparam>
    /// <typeparam name="TU">$$(HLink)$$ ViewModel.</typeparam>
    /// <typeparam name="TH"></typeparam>
    public interface IDataLayerBase<T, TU, TH>
        where TH : HLinkDBBaseCollection<TU>, new()
        where T : DBModelBase, new()
        where TU : HLinkDBBase, new()
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
        IReadOnlyList<T> DataAsDefaultSort
        {
            get;
        }

        IReadOnlyList<T> DataAsList
        {
            get;
        }

        TH GetLatestChanges
        {
            get;
        }

        DBCardGroupHLink<TU> AsCardGroup(IReadOnlyList<TU> argReadOnlyList);

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
        T GetModelFromHLink(HLinkDBBase HLink);

        /// <summary>
        /// Gets the specified h link string.
        /// </summary>
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
        CardListLineCollection GetModelInfoFormatted(DBModelBase argModel);

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        TH HLinkCollectionSort(TH collectionArg);

        void ResetCache();

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