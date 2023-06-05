// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Collections;
using GrampsView.Data.External.StoreFile;
using GrampsView.Data.Model;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels.Date;
using GrampsView.Models.DataModels.Interfaces;

using System.Collections;

namespace GrampsView.Models.DataModels
{
    //// gramps XML 1.71
    ////
    //// "primary-object"
    //// "file"
    //// - "src"
    //// - "mime"
    //// - "checksum"
    //// - "description"
    //// "attribute"
    //// "noteref"
    //// "date-content"
    //// "citationref"
    //// "tagref"

    /// <summary>
    /// Data model for a media object.
    /// </summary>

    public sealed class MediaModel : ModelBase, IMediaModel, IComparable, IComparer
    {
        private string _FileContentType = string.Empty;

        private HLinkNoteModelCollection _NoteReferenceCollection = new();

        /// <summary>
        /// The local original file path.
        /// </summary>
        private string _OriginalFilePath = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaModel"/> class.
        /// </summary>
        public MediaModel()
        {
            ModelItemGlyph.Symbol = Constants.IconMedia;
            ModelItemGlyph.SymbolColour = SharedSharpGeneral.GetResourceColour("CardBackGroundMedia");
        }

        /// <summary>
        /// Gets or sets the media storage file. Serialised separately.
        /// </summary>
        /// <value>
        /// The media storage file.
        /// </value>
        public FileInfoEx CurrentStorageFile
        {
            get; set;
        }

        public string FileContentType
        {
            get => _FileContentType;

            set
            {
                // TODO Gramps does not set the correct mime type sometimes so need a better way of
                // handling it. Maybe FileInfo Content Type
                if (value != null)
                {
                    _ = SetProperty(ref _FileContentType, value);

                    FileMimeType = SharedSharpGeneral.MimeMimeTypeGet(value);

                    FileMimeSubType = SharedSharpGeneral.MimeMimeSubTypeGet(value);
                }
            }
        }

        public string FileMimeSubType { get; set; }
            = string.Empty;

        public string FileMimeType { get; set; }
            = string.Empty;

        public HLinkCitationModelCollection GCitationRefCollection { get; set; }
                = new HLinkCitationModelCollection();

        public DateObjectModelBase GDateValue { get; set; }
            = new DateObjectModelBase();

        public string GDescription { get; set; }
            = string.Empty;

        public HLinkNoteModelCollection GNoteRefCollection
        {
            get => _NoteReferenceCollection;

            set => SetProperty(ref _NoteReferenceCollection, value);
        }

        public HLinkTagModelCollection GTagRefCollection { get; set; } = new HLinkTagModelCollection();

        public HLinkMediaModel HLink
        {
            get
            {
                HLinkMediaModel t = new()
                {
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };

                if (HLinkKey.Value == "_eecc89181d268434d7a471cc8e1")
                {
                }

                return t;
            }
        }

        public HLinkKey InternalMediaFileOriginalHLink { get; set; }
            = new HLinkKey();

        public bool IsImage => FileMimeType == "image";

        public bool IsInternalMediaFile { get; set; }

        /// <summary>
        /// Gets a value indicating whether [original file path] is valid.
        /// </summary>
        /// <value>
        /// <c> true </c> if [original file path valid]; otherwise, <c> false </c>.
        /// </value>
        public bool IsOriginalFilePathValid => !string.IsNullOrEmpty(OriginalFilePath) && StoreFileUtility.IsRelativeFilePathValid(OriginalFilePath);

        /// <summary>
        /// Gets a value indicating whether this instance is media file.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is media file; otherwise, <c> false </c>.
        /// </value>
        public CommonEnums.HLinkGlyphType MediaDisplayType
        {
            get
            {
                switch (FileMimeType)
                {
                    case "image":
                        {
                            return CommonEnums.HLinkGlyphType.Image;
                        }

                    case "video":
                        {
                            return CommonEnums.HLinkGlyphType.Media;
                        }

                    default:
                        {
                            return CommonEnums.HLinkGlyphType.Symbol;
                        }
                }
            }
        }

        public double MetaDataHeight
        {
            get; set;
        }

        public double MetaDataWidth
        {
            get; set;
        }

        public string OriginalFilePath
        {
            get => _OriginalFilePath;

            set
            {
                if (value != null)
                {
                    _ = SetProperty(ref _OriginalFilePath, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the height of the image metadata.
        /// </summary>
        /// <value>
        /// The height of the meta data.
        /// </value>
        /// <summary>
        /// Gets or sets the width of the image metadata.
        /// </summary>
        /// <value>
        /// The width of the meta data.
        /// </value>
        /// <summary>
        /// Gets or sets the original file path.
        /// </summary>
        /// <value>
        /// The original file path.
        /// </value>
        /// <summary>
        /// Gets the title decoded.
        /// </summary>
        /// <value>
        /// The title decoded.
        /// </value>
        public string TitleDecoded => GDateValue.ShortDate + " - " + GDescription;

        /// <summary>
        /// Compares the specified a.
        /// </summary>
        /// <param name="argFirstMediaModel">
        /// a.
        /// </param>
        /// <param name="argSecondMediaModel">
        /// The b.
        /// </param>
        /// <returns>
        /// </returns>
        public new int Compare(object argFirstMediaModel, object argSecondMediaModel)
        {
            if (argFirstMediaModel is null)
            {
                throw new ArgumentNullException(nameof(argFirstMediaModel));
            }

            if (argSecondMediaModel is null)
            {
                throw new ArgumentNullException(nameof(argSecondMediaModel));
            }

            IMediaModel firstMediaModel = (MediaModel)argFirstMediaModel;

            IMediaModel secondMediaModel = (MediaModel)argSecondMediaModel;

            // Compare on date first
            int testFlag = DateTime.Compare(firstMediaModel.GDateValue.SortDate, secondMediaModel.GDateValue.SortDate);

            // If the same then on Description. Usual if there is no Date
            if (testFlag.Equals(0))
            {
                testFlag = string.Compare(firstMediaModel.GDescription, secondMediaModel.GDescription, StringComparison.CurrentCulture);
            }

            return testFlag;
        }

        /// <summary>
        /// Implement IComparable CompareTo method.
        /// </summary>
        /// <param name="argSecondObject">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public override int CompareTo(object argSecondObject)
        {
            if (argSecondObject is null)
            {
                throw new ArgumentNullException(nameof(argSecondObject));
            }

            IMediaModel secondMediaModel = (MediaModel)argSecondObject;

            // compare on Date first
            int testFlag = DateTime.Compare(GDateValue.SortDate, secondMediaModel.GDateValue.SortDate);

            // If the same then on Description. Usual if there is no date
            if (testFlag.Equals(0))
            {
                testFlag = string.Compare(GDescription, secondMediaModel.GDescription, StringComparison.CurrentCulture);
            }

            return testFlag;
        }

        /// <summary>
        /// Gets the default text for media which is the first fourty characters.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        public override string ToString()
        {
            return GDescription[..Math.Min(40, GDescription.Length)];
        }
    }
}