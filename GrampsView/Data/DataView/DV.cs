// <copyright file="DV.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using GrampsView.Data.Collections;
using GrampsView.Data.Repository;

namespace GrampsView.Data.DataView
{
    /// <summary>
    /// </summary>
    public static class DV
    {
        /// <summary>
        /// Gets the Address DataView.
        /// </summary>
        /// <value>
        /// The Address DataView.
        /// </value>
        public static IAddressDataView AddressDV { get; } = new AddressDataView();

        /// <summary>
        /// Gets the book mark dv.
        /// </summary>
        /// <value>
        /// The book mark dv.
        /// </value>
        public static HLinkBackLinkModelCollection BookMarkCollection
        {
            get
            {
                return DataStore.DS.BookMarkCollection;
            }
        }

        /// <summary>
        /// Gets the citation dv.
        /// </summary>
        /// <value>
        /// The citation dv.
        /// </value>
        public static ICitationDataView CitationDV { get; } = new CitationDataView();

        /// <summary>
        /// Gets the event dv.
        /// </summary>
        /// <value>
        /// The event dv.
        /// </value>
        public static IEventDataView EventDV { get; } = new EventDataView();

        /// <summary>
        /// Gets the family dv.
        /// </summary>
        /// <value>
        /// The family dv.
        /// </value>
        public static IFamilyDataView FamilyDV { get; } = new FamilyDataView();

        /// <summary>
        /// Gets the header dv.
        /// </summary>
        /// <value>
        /// The header dv.
        /// </value>
        public static IHeaderDataView HeaderDV { get; } = new HeaderDataView();

        /// <summary>
        /// Gets the media dv.
        /// </summary>
        /// <value>
        /// The media dv.
        /// </value>
        public static IMediaDataView MediaDV { get; } = new MediaDataView();

        /// <summary>
        /// Gets the name map dv.
        /// </summary>
        /// <value>
        /// The name map dv.
        /// </value>
        public static INameMapDataView NameMapDV { get; } = new NameMapDataView();

        /// <summary>
        /// Gets the note dv.
        /// </summary>
        /// <value>
        /// The note dv.
        /// </value>
        public static INoteDataView NoteDV { get; } = new NoteDataView();

        /// <summary>
        /// Gets the person dv.
        /// </summary>
        /// <value>
        /// The person dv.
        /// </value>
        public static IPersonDataView PersonDV { get; } = new PersonDataView();

        public static IPersonNameDataView PersonNameDV { get; } = new PersonNameDataView();

        /// <summary>
        /// Gets the place dv.
        /// </summary>
        /// <value>
        /// The place dv.
        /// </value>
        public static IPlaceDataView PlaceDV { get; } = new PlaceDataView();

        /// <summary>
        /// Gets the repository dv.
        /// </summary>
        /// <value>
        /// The repository dv.
        /// </value>
        public static IRepositoryDataView RepositoryDV { get; } = new RepositoryDataView();

        /// <summary>
        /// Gets the source dv.
        /// </summary>
        /// <value>
        /// The source dv.
        /// </value>
        public static ISourceDataView SourceDV { get; } = new SourceDataView();

        /// <summary>
        /// Gets the tag dv.
        /// </summary>
        /// <value>
        /// The tag dv.
        /// </value>
        public static ITagDataView TagDV { get; } = new TagDataView();
    }
}