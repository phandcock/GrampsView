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
        HLinkCitationModelCollection GCitationRefCollection { get; set; }

        string GCode { get; set; }

        string GCoordLat { get; set; }

        string GCoordLong { get; set; }

        PlaceLocationCollection GLocation { get; set; }

        HLinkMediaModelCollection GMediaRefCollection { get; set; }

        HLinkNoteModelCollection GNoteRefCollection { get; set; }

        PlaceNameModelCollection GPlaceNames { get; set; }

        HLinkPlaceModelCollection GPlaceRefCollection { get; set; }

        HLinkTagModelCollection GTagRefCollection { get; set; }

        //string GPTitle { get; set; }
        string GType { get; set; }

        OCURLModelCollection GURLCollection { get; set; }

        HLinkPlaceModel HLink
        {
            get;
        }

        HLinkPlaceModelCollection PlaceChildCollection { get; set; }
    }
}