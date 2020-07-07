//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="IDataViewBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using System.Collections.Generic;

    using GrampsView.Common;
    using GrampsView.Data.Model;

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
        int Count { get; }

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

        IReadOnlyList<T> DataViewData { get; }

        List<CommonGroupInfoCollection<T>> GetGroupsByLetter
        {
            get;
        }

        CardGroup AsCardGroup(IReadOnlyList<TU> argReadOnlyList);

        CardGroup GetAllAsCardGroup();

        /// <summary>
        /// Gets all as ViewModel.
        /// </summary>
        /// <returns>
        /// </returns>
        List<T> GetAllAsModel();

        /// <summary>
        /// Gets the first image from collection.
        /// </summary>
        /// <param name="theCollection">
        /// The collection.
        /// </param>
        /// <returns>
        /// </returns>
        HLinkHomeImageModel GetFirstImageFromCollection(TH theCollection);

        CardGroup GetLatestChanges();

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
        T GetModelFromHLinkString(string HLinkString);

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
        /// News this instance.
        /// </summary>
        /// <returns>
        /// </returns>
        T NewModel();

        List<SearchItem> Search(string queryString);
    }
}