using CommunityToolkit.Mvvm.Messaging;

using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Data.DataView;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchViewModel"/> class.
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

        public HLinkAddressModelCollection SearchAddressCollection { get; set; } = new HLinkAddressModelCollection();

        //= new Group<object>();
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

        //public Group<object> ItemsFoundList
        //{
        //    get; set;
        //}
        public HLinkCitationModelCollection SearchCitationCollection { get; set; } = new HLinkCitationModelCollection();

        //public bool IsBusy
        //{
        //    get => _isBusy;
        //    set
        //    {
        //        if (_isBusy != value)
        //        {
        //            _isBusy = value;
        //            SearchButtonCommand.RaiseCanExecuteChanged();
        //        }
        //    }
        //}
        public HLinkEventModelCollection SearchEventsCollection { get; set; } = new HLinkEventModelCollection();

        public HLinkFamilyModelCollection SearchFamilyCollection { get; set; } = new HLinkFamilyModelCollection();

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

        public HLinkNoteModelCollection SearchNoteCollection { get; set; } = new HLinkNoteModelCollection();

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
                SearchCitationCollection = DV.CitationDV.Search(SearchText);
                SearchEventsCollection = DV.EventDV.Search(SearchText);
                SearchFamilyCollection = DV.FamilyDV.Search(SearchText);
                SearchMediaCollection = DV.MediaDV.Search(SearchText);
                SearchNoteCollection = DV.NoteDV.Search(SearchText);
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