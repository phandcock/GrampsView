// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Collections;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.HLinks.Models;
using GrampsView.ModelsDB.Collections.HLinks;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Public interfaces for the Place model
    /// </summary>
    public interface IPlaceModel : IModelBase
    {
        HLinkCitationDBModelCollection GCitationRefCollection
        {
            get;
        }

        string GCode
        {
            get; set;
        }

        double GCoordLat
        {
            get; set;
        }

        double GCoordLong
        {
            get; set;
        }

        HLinkPlaceLocationCollection GLocation
        {
            get;
        }

        HLinkMediaModelCollection GMediaRefCollection
        {
            get;
        }

        HLinkNoteDBModelCollection GNoteRefCollection
        {
            get;
        }

        HLinkPlaceNameModelCollection GPlaceNames
        {
            get;
        }

        HLinkPlaceModelCollection GPlaceParentCollection
        {
            get;
        }

        HLinkTagModelCollection GTagRefCollection
        {
            get;
        }

        //string GPTitle { get; set; }
        string GType
        {
            get;
        }

        HLinkURLModelCollection GURLCollection
        {
            get;
        }

        HLinkPlaceModel HLink
        {
            get;
        }

        HLinkPlaceModelCollection PlaceChildCollection
        {
            get;
        }
    }
}