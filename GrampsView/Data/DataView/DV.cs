// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Collections;
using GrampsView.Data.Repository;

namespace GrampsView.Data.DataView
{
    /// <summary>
    /// </summary>
    public static class DV
    {
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
                return DataStore.Instance.DS.BookMarkCollection;
            }
        }

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