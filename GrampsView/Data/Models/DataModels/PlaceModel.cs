// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// Data model for a place.
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// <description> Done </description>
    /// </item>
    /// </list>
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.ModelBase"/>
    /// <seealso cref="GrampsView.Data.ViewModel.IPlaceModel"/>
    /// <seealso cref="System.IComparable"/>
    /// <seealso cref="System.Collections.IComparer"/>
    [DataContract]
    public sealed class PlaceModel : ModelBase, IPlaceModel, IComparable, IComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceModel"/> class.
        /// </summary>
        public PlaceModel()
        {
            ModelItemGlyph.Symbol = CommonConstants.IconPlace;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPlace");

            PlaceChildCollection.Title = "Enclosed Places";
            GPlaceParentCollection.Title = "Enclosing Place";
        }

        /// <summary>
        /// Gets the default text for this Model.
        /// </summary>
        /// <value>
        /// The default text.
        /// </value>
        public override string DefaultText
        {
            get
            {
                if (!string.IsNullOrEmpty(GPTitle))
                {
                    return GPTitle;
                }

                return GPlaceNames.DefaultText;
            }
        }

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection { get; set; } = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the gp code.
        /// </summary>
        /// <value>
        /// The gp code.
        /// </value>
        [DataMember]
        public string GCode
        {
            get;

            set;
        }

        [DataMember]
        public double GCoordLat
        {
            get; set;
        }

        [DataMember]
        public double GCoordLong
        {
            get; set;
        }

        [DataMember]
        public PlaceLocationCollection GLocation { get; set; } = new PlaceLocationCollection();

        /// <summary>
        /// Gets or sets the media reference collection.
        /// </summary>
        /// <value>
        /// The media reference collection.
        /// </value>
        [DataMember]
        public HLinkMediaModelCollection GMediaRefCollection
        {
            get;

            set;
        } = new HLinkMediaModelCollection();

        /// <summary>
        /// Gets or sets the g note reference collection.
        /// </summary>
        /// <value>
        /// The g note reference collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection
        {
            get;

            set;
        } = new HLinkNoteModelCollection();

        [DataMember]
        public HLinkPlaceNameModelCollection GPlaceNames
        {
            get;

            set;
        } = new HLinkPlaceNameModelCollection();

        /// <summary>
        /// Gets or sets the g place reference collection.
        /// </summary>
        /// <value>
        /// The g place reference collection.
        /// </value>
        [DataMember]
        public HLinkPlaceModelCollection GPlaceParentCollection
        {
            get;

            set;
        } = new HLinkPlaceModelCollection();

        ///// <summary>
        ///// Gets or sets the place title.
        ///// </summary>
        ///// <value>
        ///// The gp title.
        ///// </value>
        [DataMember]
        public string GPTitle
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the tag reference collection.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection
        {
            get
            ;

            set;
        } = new HLinkTagModelCollection();

        [DataMember]
        public string GType
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets uRL model collection.
        /// </summary>
        [DataMember]
        public HLinkURLModelCollection GURLCollection
        {
            get; set;
        }

        = new HLinkURLModelCollection();

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkPlaceModel HLink
        {
            get
            {
                HLinkPlaceModel t = new HLinkPlaceModel
                {
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };
                return t;
            }
        }

        [DataMember]
        public HLinkPlaceModelCollection PlaceChildCollection
        {
            get;

            set;
        } = new HLinkPlaceModelCollection();

        /// <summary>
        /// Compares two objects.
        /// </summary>
        /// <param name="a">
        /// object A.
        /// </param>
        /// <param name="b">
        /// object B.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        int IComparer.Compare(object a, object b)
        {
            PlaceModel firstEvent = (PlaceModel)a;
            PlaceModel secondEvent = (PlaceModel)b;

            int testFlag = string.Compare(firstEvent.DefaultText, secondEvent.DefaultText, StringComparison.CurrentCulture);

            return testFlag;
        }

        /// <summary>
        /// Implement IComparable CompareTo method.
        /// </summary>
        /// <param name="obj">
        /// The object to compare.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        int IComparable.CompareTo(object obj)
        {
            PlaceModel secondEvent = (PlaceModel)obj;

            int testFlag = string.Compare(DefaultText, secondEvent.DefaultText, StringComparison.CurrentCulture);

            return testFlag;
        }

        public MapModel ToMapModel()
        {
            MapModel newMapModel = new MapModel
            {
                Description = DefaultText,
            };

            // Try Lat-Long first
            if (GCoordLat != 0.0 && GCoordLong != 0.0)
            {
                newMapModel.MapType = MapType.LatLong;

                newMapModel.myLocation.Latitude = GCoordLat;
                newMapModel.myLocation.Longitude = GCoordLong;

                return newMapModel;
            }

            return newMapModel;
        }
    }
}