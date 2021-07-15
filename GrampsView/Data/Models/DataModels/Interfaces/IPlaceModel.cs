namespace GrampsView.Data.Model
{
    using GrampsView.Data.Collections;

    /// <summary>
    /// Public interfaces for the Tag elements.
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

        string GCoordLat
        {
            get; set;
        }

        string GCoordLong
        {
            get; set;
        }

        PlaceLocationCollection GLocation
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