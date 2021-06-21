namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Collections;

    public interface IMediaModel : IModelBase
    {
        string FileContentType
        {
            get;
            set;
        }

        string FileMimeSubType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the file MIME.
        /// </summary>
        /// <value>
        /// The file MIME.
        /// </value>
        string FileMimeType
        {
            get;
            set;
        }

        HLinkCitationModelCollection GCitationRefCollection
        {
            get;
        }

        /// <summary>
        /// Gets or sets the date value.
        /// </summary>
        /// <value>
        /// The date value.
        /// </value>
        IDateObjectModel GDateValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the file description.
        /// </summary>
        /// <value>
        /// The file description.
        /// </value>
        string GDescription
        {
            get;
            set;
        }

        HLinkNoteModelCollection GNoteRefCollection
        {
            get;
        }

        HLinkTagModelCollection GTagRefCollection
        {
            get;
        }

        /// <summary>
        /// Gets the get h link Media Model that points to this ViewModel.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        HLinkMediaModel HLink
        {
            get;
        }

        HLinkKey InternalMediaFileOriginalHLink
        {
            get;
            set;
        }

        bool IsImage
        {
            get;
        }

        bool IsInternalMediaFile
        {
            get; set;
        }

        bool IsMediaStorageFileValid
        {
            get;
        }

        bool IsOriginalFilePathValid
        {
            get;
        }

        CommonEnums.HLinkGlyphType MediaDisplayType
        {
            get;
        }

        FileInfoEx MediaStorageFile
        {
            get;

            set;
        }

        /// <summary>
        /// Gets the media storage file path.
        /// </summary>
        /// <value>
        /// The media storage file path.
        /// </value>
        string MediaStorageFilePath
        {
            get;
        }

        /// <summary>
        /// Gets or sets the height of the meta data.
        /// </summary>
        /// <value>
        /// The height of the meta data.
        /// </value>
        double MetaDataHeight
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the width of the meta data.
        /// </summary>
        /// <value>
        /// The width of the meta data.
        /// </value>
        double MetaDataWidth
        {
            get; set;
        }

        string OriginalFilePath
        {
            get;

            set;
        }

        IMediaModel Clone();

        /// <summary>
        /// Cleans this instance.
        /// </summary>
        void FullImageClean();
    }
}