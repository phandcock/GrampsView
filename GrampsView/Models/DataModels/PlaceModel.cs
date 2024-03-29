﻿// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Data.Model;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels.Interfaces;
using GrampsView.Models.DataModels.Minor;
using GrampsView.Models.HLinks.Models;
using GrampsView.ModelsDB.Collections.HLinks;

using System.Collections;



using static GrampsView.Common.CommonEnums;

namespace GrampsView.Models.DataModels
{
    /// <summary>
    /// Data model for a place.
    /// <list type="table"><listheader><term> Item </term><term> Status </term></listheader><item><description> XML 1.71 check </description><description> Done </description></item></list></summary>
    /// <seealso cref="ModelBase" />
    /// <seealso cref="IPlaceModel" />
    /// <seealso cref="IComparable" />
    /// <seealso cref="IComparer" />

    public sealed class PlaceModel : ModelBase, IPlaceModel, IComparable, IComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceModel"/> class.
        /// </summary>
        public PlaceModel()
        {
            ModelItemGlyph.Symbol = Constants.IconPlace;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundPlace");
        }

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>

        public HLinkCitationDBModelCollection GCitationRefCollection { get; set; } = new HLinkCitationDBModelCollection();

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

        public HLinkNoteDBModelCollection GNoteRefCollection
        {
            get;

            set;
        } = new HLinkNoteDBModelCollection();

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
        } = new HLinkPlaceModelCollection()
        {
            Title = "Enclosing Place",
        };

        ///// <summary>
        ///// Gets or sets the place name.
        ///// </summary>
        ///// <value>
        ///// The gp title.
        ///// </value>

        public string GPName
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
                HLinkPlaceModel t = new()
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
        } = new HLinkPlaceModelCollection()
        {
            Title = "Enclosed Places",
        };

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
            IMapModel newMapModel = new MapModel
            {
                Description = GPName
            };

            // Try Lat-Long first
            if (GCoordLat != 0.0 && GCoordLong != 0.0)
            {
                newMapModel.MapType = MapType.LatLong;

                newMapModel.MyLocation.Latitude = GCoordLat;
                newMapModel.MyLocation.Longitude = GCoordLong;

                return newMapModel;
            }

            // Default to place
            PlaceModel thisPlaceModel = this;

            // Walk the hierarchy to the top to give Maps something to search for
            Placemark currentPlace = new();

            if (!string.IsNullOrEmpty(GPName))
            {
                currentPlace.Thoroughfare = $"{GPName}";
                newMapModel.Description = $"{GPName}";
            }
            else
            {
                if (!string.IsNullOrEmpty(GPlaceNames[0].DeRef.DefaultTextShort))
                {
                    currentPlace.Thoroughfare = $"{GPlaceNames[0].DeRef.DefaultTextShort}";
                    newMapModel.Description = $"{GPlaceNames[0].DeRef.DefaultTextShort}";
                };
            }

            while (thisPlaceModel.GPlaceParentCollection.Count > 0)
            {
                thisPlaceModel = thisPlaceModel.GPlaceParentCollection[0].DeRef;

                switch (thisPlaceModel.GType)
                {
                    case "Country":
                        {
                            currentPlace.CountryName = $"{thisPlaceModel.DefaultTextShort}";
                            break;
                        }
                    case "City":
                    case "Locality":
                    case "Town":
                        {
                            currentPlace.Locality = $"{thisPlaceModel.DefaultTextShort}";
                            break;
                        }
                    case "County":
                        {
                            currentPlace.SubAdminArea = $"{thisPlaceModel.DefaultTextShort}";
                            break;
                        }
                    case "State":
                        {
                            currentPlace.AdminArea = $"{thisPlaceModel.DefaultTextShort}";
                            break;
                        }
                    default:
                        {
                            // TODO Display alert for unknown Type
                            break;
                        }
                }
            }

            newMapModel.MyPlaceMark = currentPlace;
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

            return !string.IsNullOrEmpty(GPName) ? GPName : GPlaceNames.ToString();
        }
    }
}