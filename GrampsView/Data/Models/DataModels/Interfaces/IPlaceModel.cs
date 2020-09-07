//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="IPlaceModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using GrampsView.Data.Collections;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Public interfaces for the Tag elements.
    /// </summary>
    public interface IPlaceModel : IModelBase
    {
        HLinkCitationModelCollection GCitationRefCollection { get; }

        string GCode { get; set; }

        string GCoordLat { get; set; }

        string GCoordLong { get; set; }

        PlaceLocationCollection GLocation { get; }

        HLinkMediaModelCollection GMediaRefCollection { get; }

        HLinkNoteModelCollection GNoteRefCollection { get; }

        PlaceNameModelCollection GPlaceNames { get; }

        HLinkPlaceModelCollection GPlaceRefCollection { get; }

        HLinkTagModelCollection GTagRefCollection { get; }

        //string GPTitle { get; set; }
        string GType { get;  }

        OCURLModelCollection GURLCollection { get; }

        HLinkPlaceModel HLink
        {
            get;
        }

        HLinkPlaceModelCollection PlaceChildCollection { get; }
    }
}