// <copyright file="DataViewBase.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Partially based on http://stackoverflow.com/questions/8157140/net-4-0-indexer-with-observablecollection.
    /// </summary>
    /// <typeparam name="TB">
    /// HLinkCollection.
    /// </typeparam>
    /// <typeparam name="TU">
    /// ModelBase.
    /// </typeparam>
    /// <typeparam name="TH">
    /// Hlink.
    /// </typeparam>
    /// <seealso cref="GrampsView.Common.CommonBindableBase"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.DataView.IDataViewBase{T, U, H}"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.ComponentViewModel.INotifyPropertyChanged"/>
    public abstract class DataViewBase<TB, TU, TH> : CommonBindableBase, IDataViewBase<TB, TU, TH>, INotifyPropertyChanged
        where TH : HLinkBaseCollection<TU>, new()
        where TB : ModelBase, new()
        where TU : HLinkBase, new()
    {
        public int Count
        {
            get
            {
                return DataViewData.Count;
            }
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public abstract IReadOnlyList<TB> DataDefaultSort
        {
            get;
        }

        /// <summary>
        /// Gets or sets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public virtual IReadOnlyList<TB> DataViewData
        {
            get;
        }

        /// <summary>
        /// Gets the get groups by letter. Default to empty list.
        /// </summary>
        /// <value>
        /// The get groups by letter.
        /// </value>
        public virtual List<CommonGroupInfoCollection<TB>> GetGroupsByLetter
        {
            get
            {
                return new List<CommonGroupInfoCollection<TB>>();
            }
        }

        public virtual CardGroup AsCardGroup(IReadOnlyList<TU> argReadOnlyList)
        {
            CardGroup t = new CardGroup();

            foreach (var item in argReadOnlyList)
            {
                t.Add(item);
            }

            return t;
        }

        public abstract CardGroupBase<TU> GetAllAsCardGroup();

        /// <summary>
        /// Gets all as ViewModel.
        /// </summary>
        /// <returns>
        /// List of models.
        /// </returns>
        public List<TB> GetAllAsModel()
        {
            return DataViewData.OrderBy(t => t).ToList();
        }

        /// <summary>
        /// Gets the first image from collection.
        /// </summary>
        /// <param name="theCollection">
        /// The collection.
        /// </param>
        /// <returns>
        /// A HLink Media Model of the first image in the collection or null if none are found.
        /// </returns>
        public virtual HLinkHomeImageModel GetFirstImageFromCollection(TH theCollection)
        {
            throw new NotImplementedException();
        }

        public abstract CardGroupBase<TU> GetLatestChanges();

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="argHLink">
        /// The hlink.
        /// </param>
        /// <returns>
        /// Model for HLink.
        /// </returns>
        public TB GetModelFromHLink(HLinkBase argHLink)
        {
            if (argHLink is null)
            {
                throw new ArgumentNullException(nameof(argHLink));
            }

            return this.GetModelFromHLinkString(argHLink.HLinkKey);
        }

        /// <summary>
        /// Gets the specified h link string.
        /// </summary>
        /// <param name="HLinkString">
        /// The h link string.
        /// </param>
        /// <returns>
        /// ModelBase.
        /// </returns>
        public abstract TB GetModelFromHLinkString(string HLinkString);

        /// <summary>
        /// Gets the model information formatted.
        /// </summary>
        /// <param name="argModel">
        /// The argument ViewModel.
        /// </param>
        /// <returns>
        /// A Card List Model object.
        /// </returns>
        public CardListLineCollection GetModelInfoFormatted(ModelBase argModel)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            CardListLineCollection
               modelInfoList = new CardListLineCollection
               {
                 new CardListLine("Handle:", argModel.Handle),
                 new CardListLine("Id:", argModel.Id),
                 new CardListLine("Change:", argModel.Change.ToString(System.Globalization.CultureInfo.CurrentCulture)),
                 new CardListLine("Private Object:", argModel.PrivAsString),
               };

            return modelInfoList;
        }

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// The argument unsorted.
        /// </returns>
        public virtual TH HLinkCollectionSort(TH collectionArg)
        {
            return collectionArg;
        }

        /// <summary>
        /// New instance of ViewModel.
        /// </summary>
        /// <returns>
        /// New model instance.
        /// </returns>
        public virtual TB NewModel()
        {
            TB t = new TB();

            return t;
        }

        /// <summary>
        /// Searches the items.
        /// </summary>
        /// <param name="argQuery">
        /// The query string.
        /// </param>
        /// <returns>
        /// </returns>
        public abstract CardGroupBase<TU> Search(string argQuery);
    }
}