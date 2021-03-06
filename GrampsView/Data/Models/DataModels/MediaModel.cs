// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Collections;
    using System.Runtime.Serialization;

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
    [DataContract]
    public sealed class MediaModel : ModelBase, IMediaModel, IComparable, IComparer
    {
        /// <summary>
        /// The local date value.
        /// </summary>
        private IDateObjectModel _DateValue = new DateObjectModelVal();

        private string _FileContentType;

        /// <summary>
        /// My file description.
        /// </summary>
        private string _FileDescription = string.Empty;

        private bool _IsClippedFile;

        /// <summary>
        /// Local Storage File for media object.
        /// </summary>
        private FileInfoEx _MediaStorageFile;

        private HLinkNoteModelCollection _NoteReferenceCollection = new HLinkNoteModelCollection();

        /// <summary>
        /// The local original file path.
        /// </summary>
        private string _OriginalFilePath = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaModel"/> class.
        /// </summary>
        public MediaModel()
        {
            ModelItemGlyph.Symbol = CommonConstants.IconMedia;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundMedia");
        }

        /// <summary>
        /// Gets or sets the type of the file content.
        /// </summary>
        /// <value>
        /// The type of the file content.
        /// </value>
        [DataMember]
        public string FileContentType
        {
            get
            {
                return _FileContentType;
            }

            set
            {
                // TODO Gramps does not set the correct mimetype sometimes so need a better way of
                // handling it. Maybe FileInfo COntent Type
                if (value != null)
                {
                    SetProperty(ref _FileContentType, value);

                    FileMimeType = CommonRoutines.MimeMimeTypeGet(value);

                    FileMimeSubType = CommonRoutines.MimeMimeSubTypeGet(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the MIME subtype of the file.
        /// </summary>
        /// <value>
        /// The type of the file MIME sub.
        /// </value>
        [DataMember]
        public string FileMimeSubType
        {
            get; set;
        }

        /// <summary> Gets or sets the file MIME type. </summary> <value> The file MIME type.
        [DataMember]
        public string FileMimeType
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the citation reference collection.
        /// </summary>
        /// <value>
        /// The citation reference collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection
        {
            get;
            set;
        }

        = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the date value.
        /// </summary>
        /// <value>
        /// The date value.
        /// </value>
        [DataMember]
        public IDateObjectModel GDateValue
        {
            get
            {
                return _DateValue;
            }

            set
            {
                SetProperty(ref _DateValue, value);
            }
        }

        /// <summary>
        /// Gets or sets the file description.
        /// </summary>
        /// <value>
        /// The file description.
        /// </value>
        [DataMember]
        public string GDescription
        {
            get
            {
                return _FileDescription;
            }

            set
            {
                SetProperty(ref _FileDescription, value);
            }
        }

        /// <summary>
        /// Gets the default text for media which is the first fourty characters.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        public new string GetDefaultText
        {
            get
            {
                return GDescription.Substring(0, Math.Min(40, GDescription.Length));
            }
        }

        /// <summary>
        /// Gets or sets the note reference collection.
        /// </summary>
        /// <value>
        /// The note reference collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection
        {
            get
            {
                return _NoteReferenceCollection;
            }

            set
            {
                SetProperty(ref _NoteReferenceCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the tag reference collection.
        /// </summary>
        /// <value>
        /// The tag reference collection.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection { get; set; } = new HLinkTagModelCollection();

        /// <summary>
        /// Gets the hlink.
        /// </summary>
        /// <value>
        /// The get hlink.
        /// </value>
        public HLinkMediaModel HLink
        {
            get
            {
                HLinkMediaModel t = new HLinkMediaModel
                {
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };
                return t;
            }
        }

        [DataMember]
        public bool IsClippedFile
        {
            get
            {
                return _IsClippedFile;
            }

            set
            {
                SetProperty(ref _IsClippedFile, value);
            }
        }

        public bool IsImage
        {
            get
            {
                return (FileMimeType == "image");
            }
        }

        /// <summary>
        /// Gets a value indicating whether [media storage file valid]. Runs various checks on the mediafile.
        /// </summary>
        /// <value>
        /// <c>true</c> if [media storage file valid]; otherwise, <c>false</c>.
        /// </value>
        public bool IsMediaStorageFileValid
        {
            get
            {
                // TODO Enhance to check for zero length files
                if (MediaStorageFile == null)
                {
                    return false;
                };

                if (!MediaStorageFile.Valid)
                {
                    return false;
                };

                if (_MediaStorageFile.FInfo.FullName == null)
                {
                    return false;
                };

                //if (!(Uri.IsWellFormedUriString(MediaStorageFileUri.AbsolutePath, UriKind.Absolute))) { return false; };

                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [original file path] is valid.
        /// </summary>
        /// <value>
        /// <c>true</c> if [original file path valid]; otherwise, <c>false</c>.
        /// </value>
        public bool IsOriginalFilePathValid
        {
            get
            {
                if (string.IsNullOrEmpty(OriginalFilePath))
                {
                    return false;
                }

                return StoreFileUtility.IsRelativeFilePathValid(OriginalFilePath);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is media file.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is media file; otherwise, <c>false</c>.
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

        /// <summary>
        /// Gets or sets the media storage file. Serialised separately.
        /// </summary>
        /// <value>
        /// The media storage file.
        /// </value>
        public FileInfoEx MediaStorageFile
        {
            get
            {
                return _MediaStorageFile;
            }

            set
            {
                SetProperty(ref _MediaStorageFile, value);
            }
        }

        /// <summary>
        /// Gets the media storage file path or string.empty if null.
        /// </summary>
        /// <value>
        /// The media storage file.
        /// </value>
        public string MediaStorageFilePath
        {
            get
            {
                if (IsMediaStorageFileValid)
                {
                    return _MediaStorageFile.FInfo.FullName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the media storage file URI.
        /// </summary>
        /// <value>
        /// The media storage file URI.
        /// </value>
        public Uri MediaStorageFileUri
        {
            get
            {
                if (IsMediaStorageFileValid)
                {
                    return new Uri(MediaStorageFilePath);
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the height of the image metadata.
        /// </summary>
        /// <value>
        /// The height of the meta data.
        /// </value>
        [DataMember]
        public double MetaDataHeight
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the width of the image metadata.
        /// </summary>
        /// <value>
        /// The width of the meta data.
        /// </value>
        [DataMember]
        public double MetaDataWidth
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the original file path.
        /// </summary>
        /// <value>
        /// The original file path.
        /// </value>
        [DataMember]
        public string OriginalFilePath
        {
            get
            {
                return _OriginalFilePath;
            }

            set
            {
                if (value != null)
                {
                    SetProperty(ref _OriginalFilePath, value);
                }
            }
        }

        /// <summary>
        /// Gets the title decoded.
        /// </summary>
        /// <value>
        /// The title decoded.
        /// </value>
        public string TitleDecoded
        {
            get
            {
                return GDateValue.ShortDate + " - " + GDescription;
            }
        }

        public IMediaModel Clone()
        {
            IMediaModel t = new MediaModel
            {
                FileMimeType = this.FileMimeType,
                GDateValue = this.GDateValue,
                GDescription = this.GDescription,
                GNoteRefCollection = this.GNoteRefCollection,
                GTagRefCollection = this.GTagRefCollection,

                MetaDataHeight = this.MetaDataHeight,
                OriginalFilePath = this.OriginalFilePath
            };

            t.ModelItemGlyph.ImageType = this.ModelItemGlyph.ImageType;
            t.ModelItemGlyph.Symbol = this.ModelItemGlyph.Symbol;
            t.ModelItemGlyph.SymbolColour = this.ModelItemGlyph.SymbolColour;

            t.GCitationRefCollection.Clear();
            t.GCitationRefCollection.AddRange(this.GCitationRefCollection);

            return t;
        }

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
        public int CompareTo(object argSecondObject)
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
        /// Cleans this instance.
        /// </summary>
        public void FullImageClean()
        {
            // TODO fix cache
            //IsFullImageLoaded = false;
            //ImageFullBitmap = null;
        }
    }
}