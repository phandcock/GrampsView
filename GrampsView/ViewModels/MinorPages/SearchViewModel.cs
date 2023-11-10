// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Data.DataView;
using GrampsView.Models.Collections.HLinks;
using GrampsView.ModelsDB.Collections.HLinks;

using System.Diagnostics.Contracts;
using System.Globalization;

namespace GrampsView.ViewModels.MinorPages
{
    /// <summary>
    /// Search ViewModel class.
    /// </summary>
    /// <seealso cref="ViewModelBase"/>
    public class SearchPageViewModel : ViewModelBase
    {
        private string lastArg = string.Empty;

        public HLinkAddressModelCollection SearchAddressCollection { get; set; } = new HLinkAddressModelCollection();

        /// <summary>
        /// Gets the search button command.
        /// </summary>
        /// <value>
        /// The search button command.
        /// </value>
        public Command<string> SearchButtonCommand
        {
            get;
        }

        public HLinkCitationDBModelCollection SearchCitationCollection { get; set; } = new HLinkCitationDBModelCollection();

        public HLinkEventDBModelCollection SearchEventsCollection { get; set; } = new HLinkEventDBModelCollection();

        public HLinkFamilyDBModelCollection SearchFamilyCollection { get; set; } = new HLinkFamilyDBModelCollection();

        /// <summary>
        /// Gets or sets a value indicating whether [search items found].
        /// </summary>
        /// <value>
        /// <c> true </c> if [search items found]; otherwise, <c> false </c>.
        /// </value>
        public bool SearchItemsFound
        {
            get;

            set;
        } = false;

        public HLinkMediaModelCollection SearchMediaCollection { get; set; } = new HLinkMediaModelCollection();

        public HLinkNoteDBModelCollection SearchNoteCollection { get; set; } = new HLinkNoteDBModelCollection();

        public HLinkPersonModelCollection SearchPersonCollection { get; set; } = new HLinkPersonModelCollection();

        public HLinkPersonNameModelCollection SearchPersonNameCollection { get; set; } = new HLinkPersonNameModelCollection();

        public HLinkPlaceModelCollection SearchPlaceCollection { get; set; } = new HLinkPlaceModelCollection();

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>
        /// The search text.
        /// </value>
        public string SearchText
        {
            get;

            set;
        } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPageViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// Common Logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// Event Aggregator.
        /// </param>
        public SearchPageViewModel(SharedSharp.Logging.Interfaces.ILog iocCommonLogging, IMessenger iocEventAggregator)
            : base(iocCommonLogging)
        {
            BaseTitle = "Search Page";

            BaseTitleIcon = Constants.IconSearch;

            SearchButtonCommand = new Command<string>(buttonClickText => SearchProcessQuery(buttonClickText)); //, _ => !IsBusy) ;
        }

        /// <summary>
        /// Processes the search query.
        /// </summary>
        /// <param name="argSearch">
        /// Search Text.
        /// </param>
        public void ProcessQuery(string argSearch)

        {
            Contract.Assert(argSearch != null);

            SearchText = argSearch.Trim().ToLower(CultureInfo.CurrentCulture);

            if (SearchText.Length > 0)
            {
                SearchItemsFound = true;

                SearchAddressCollection = DV.AddressDV.Search(SearchText);
                SearchCitationCollection = DL.CitationDL.Search(SearchText);
                SearchEventsCollection = DL.EventDL.Search(SearchText);
                SearchFamilyCollection = DL.FamilyDL.Search(SearchText);
                SearchMediaCollection = DV.MediaDV.Search(SearchText);
                SearchNoteCollection = DL.NoteDL.Search(SearchText);
                SearchPersonCollection = DV.PersonDV.Search(SearchText);
                SearchPersonNameCollection = DV.PersonNameDV.Search(SearchText);
                SearchPlaceCollection = DV.PlaceDV.Search(SearchText);

                SearchItemsFound = SearchAddressCollection.Count +
                    SearchCitationCollection.Count +
                    SearchEventsCollection.Count +
                    SearchFamilyCollection.Count +
                    SearchMediaCollection.Count +
                    SearchNoteCollection.Count +
                    SearchPersonCollection.Count +
                    SearchPersonNameCollection.Count +
                    SearchPlaceCollection.Count
                    > 0;
            }
        }

        /// <summary>
        /// Processes the search query.
        /// </summary>
        /// <param name="argSearch">
        /// </param>
        public void SearchProcessQuery(string argSearch)
        {
            // Handle issues with bounce onf EventToCommand
            if (lastArg != argSearch)
            {
                ProcessQuery(argSearch);
            }

            lastArg = argSearch;
        }
    }
}