namespace GrampsView.Data.Model
{
    using GrampsView.Data.Collections;
    using GrampsView.Models.HLinks.Models;

    /// <summary>
    /// Public interfaces for the Place model
    /// </summary>
    public interface IPlaceModel : IModelBase
    {
        HLinkCitationModelCollection GCitationRefCollection
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

        HLinkNoteModelCollection GNoteRefCollection
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