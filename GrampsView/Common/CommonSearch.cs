//-----------------------------------------------------------------------
//
//
//
// <copyright file="CommonSearch.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{

    using GrampsView.Data.Model;

    /// <summary>
    /// Common Search Routines.
    /// </summary>
    public class CommonSearch
    {
        ///// <summary>
        ///// Generates the suggestions.
        ///// </summary>
        ///// <param name="args">
        ///// The <see cref="AutoSuggestBoxTextChangedEventArgs" /> instance containing the event data.
        ///// </param>
        ///// <param name="searchText">
        ///// The search text.
        ///// </param>
        ///// <returns>
        ///// A collection of search items.
        ///// </returns>
        //public ObservableCollection<SearchItem> GenerateSuggestions(string args, string searchText)
        //{
        //    ObservableCollection<SearchItem> searchSuggestions = new ObservableCollection<SearchItem>();

        // //if ((args.Reason == AutoSuggestionBoxTextChangeReason.UserInput) && (searchText !=
        // null)) //{ // List<SearchItem> personResults = DV.PersonDV.Search(searchText);

        // // // TODO sort t = DV.PersonDV.HLinkCollectionSort(t);

        // // searchSuggestions.AddRange(personResults);

        // // // Search notes List<SearchItem> noteResults = DV.NoteDV.Search(searchText);

        // // searchSuggestions.AddRange(noteResults);

        // // // Handle nothing found // if (searchSuggestions.Count == 0) // { //
        // searchSuggestions.Add( // new SearchItem() // { // Text = CommonConstants.SearchNoResults,
        // // HLink = null, // }); // } //}

        //    return searchSuggestions;
        //}

        /// <summary>
        /// Handle chosen suggestions via keyboard. Currenlty doe snto need to do anything.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        //public void SuggestionChosen(object sender, string parameter)
        //{
        //    //if ((parameter.SelectedItem as SearchItem).Text != CommonConstants.SearchNoResults)
        //    //{
        //    //}
        //}
    }

    /// <summary>
    /// </summary>
    public class SearchItem
    {
        public HLinkBase HLink { get; set; }

        public string Text { get; set; }
    }
}