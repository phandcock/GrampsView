namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;

    using SharedSharp.Model;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using Xamarin.CommunityToolkit.ObjectModel;

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
    /// <seealso cref="GrampsView.Common.ObservableObject"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.DataView.IDataViewBase{T, U, H}"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.ComponentViewModel.INotifyPropertyChanged"/>
    public abstract class DataViewBase<TB, TU, TH> : ObservableObject, IDataViewBase<TB, TU, TH>, INotifyPropertyChanged
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

        public virtual TH GetLatestChanges
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual CardGroupHLink<TU> AsCardGroup(IReadOnlyList<TU> argReadOnlyList)
        {
            Contract.Assert(argReadOnlyList != null);

            CardGroupHLink<TU> t = new CardGroupHLink<TU>();

            foreach (var item in argReadOnlyList)
            {
                t.Add(item);
            }

            return t;
        }

        public virtual TH GetAllAsCardGroupBase()
        {
            throw new NotImplementedException();
        }

        public abstract Group<TH> GetAllAsGroupedCardGroup();

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

        public virtual ItemGlyph GetGlyph(HLinkKey argHLinkKey)
        {
            return GetModelFromHLinkKey(argHLinkKey).ModelItemGlyph;
        }

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

            return this.GetModelFromHLinkKey(argHLink.HLinkKey);
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
        public abstract TB GetModelFromHLinkKey(HLinkKey argHLinkKey);

        public abstract TB GetModelFromId(string argId);

        /// <summary>
        /// Gets the model information formatted.
        /// </summary>
        /// <param name="argModel">
        /// The argument ViewModel.
        /// </param>
        /// <returns>
        /// A CardListLineCollection object od the basic admin and id values.
        /// </returns>
        public CardListLineCollection GetModelInfoFormatted(ModelBase argModel)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            CardListLineCollection modelInfoList = new CardListLineCollection
               {
                 new CardListLine("Id:", argModel.Id),
                 new CardListLine("Change:", argModel.Change.ToString(System.Globalization.CultureInfo.CurrentCulture)),
                 new CardListLine("Private Object:", argModel.Priv.ToString()),
                new CardListLine("Handle:", argModel.HLinkKey.Value),
               };

            modelInfoList.Title = "Admin Details";

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
        /// Searches the items.
        /// </summary>
        /// <param name="argQuery">
        /// The query string.
        /// </param>
        /// <returns>
        /// </returns>
        public abstract TH Search(string argQuery);
    }
}