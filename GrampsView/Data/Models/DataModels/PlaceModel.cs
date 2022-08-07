// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Collections;

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
    /// <seealso cref="Data.ViewModel.ModelBase"/>
    /// <seealso cref="Data.ViewModel.IPlaceModel"/>
    /// <seealso cref="IComparable"/>
    /// <seealso cref="IComparer"/>

    public sealed class PlaceModel : ModelBase, IPlaceModel, IComparable, IComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceModel"/> class.
        /// </summary>
        public PlaceModel()
        {
            ModelItemGlyph.Symbol = Constants.IconPlace;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPlace");

            PlaceChildCollection.Title = "Enclosed Places";
            GPlaceParentCollection.Title = "Enclosing Place";
        }

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>

        public HLinkCitationModelCollection GCitationRefCollection { get; set; } = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the gp code.
        /// </summary>
        /// <value>
        /// The gp code.
        /// </value>

        public string GCode
        {
            get;

            set;
        }

        public double GCoordLat
        {
            get; set;
        }

        public double GCoordLong
        {
            get; set;
        }

        public HLinkPlaceLocationCollection GLocation { get; set; } = new HLinkPlaceLocationCollection();

        /// <summary>
        /// Gets or sets the media reference collection.
        /// </summary>
        /// <value>
        /// The media reference collection.
        /// </value>

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

        public HLinkNoteModelCollection GNoteRefCollection
        {
            get;

            set;
        } = new HLinkNoteModelCollection();

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

        public HLinkTagModelCollection GTagRefCollection
        {
            get
            ;

            set;
        } = new HLinkTagModelCollection();

        public string GType
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets uRL model collection.
        /// </summary>

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

            int testFlag = string.Compare(firstEvent.ToString(), secondEvent.ToString(), StringComparison.CurrentCulture);

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

            int testFlag = string.Compare(ToString(), secondEvent.ToString(), StringComparison.CurrentCulture);

            return testFlag;
        }

        public IMapModel ToMapModel()
        {
            IMapModel newMapModel = new MapModel();
            newMapModel.Description = GPTitle;

            // Try Lat-Long first
            if (GCoordLat != 0.0 && GCoordLong != 0.0)
            {
                newMapModel.MapType = MapType.LatLong;

                newMapModel.MyLocation.Latitude = GCoordLat;
                newMapModel.MyLocation.Longitude = GCoordLong;

                return newMapModel;
            }

            // Default to place

            // Walk the hierarchy to the top to give Maps something to search for
            string currentPlace = $"{GPTitle}, {GPlaceNames[0].DeRef.DefaultTextShort}";

            PlaceModel thisPlaceModel = this;

            while (thisPlaceModel.GPlaceParentCollection.Count > 0)
            {
                thisPlaceModel = thisPlaceModel.GPlaceParentCollection[0].DeRef;

                currentPlace += $", {thisPlaceModel.DefaultTextShort}";
            }

            newMapModel.Description = currentPlace;
            newMapModel.MapType = MapType.Place;

            return newMapModel;
        }

        /// <summary>
        /// Gets the default text for this Model.
        /// </summary>
        /// <value>
        /// The default text.
        /// </value>
        public override string ToString()
        {
            // Build a complete place name.  Assumes we are usiing the hierarchy.

            if (!string.IsNullOrEmpty(GPTitle))
            {
                return GPTitle;
            }

            return GPlaceNames.ToString();
        }
    }
}